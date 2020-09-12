using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Threading.Tasks;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerAttackAssembly : AttackAssemblyBase {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; protected set; }
        #endregion

        #region {[ FIELDS ]}
        private object _rocketLock, _laserLock, _rocketLauncherLock, _rocketLauncherRocketLock;
        private long _lastRocketAttack, _lastLaserAttack, _lastRsbAttack, _lastPibAttack,
            _lastPLD8Attack, _lastDCR250Attack, _lastRIC3Attack,
            _lastRocketLauncherAttack, _lastRocketLauncherRocketReload;
        private int _lapsSinceLastRocket, _lapsSinceLastLaser,
            _lapsSinceLastRocketLauncherRocket;

        private int _outOfRangeCounter = 0, _healCounter = 0;
        private TickInterval _rocketLauncherReloadTimer;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerAttackAssembly(PlayerController controller) : base(controller) {
            _rocketLauncherReloadTimer = new TickInterval(RocketLauncherReloadTimer, 100);
            PlayerController = controller;

            _rocketLock = new object();
            _laserLock = new object();
            _rocketLauncherLock = new object();
            _rocketLauncherRocketLock = new object();
        }

        #endregion

        #region {[ HELPER ]}
        private double Noise(double value, double percent = .2) {
            double minimum = value - (value * (percent * .5));
            double maximum = value + (value * (percent * .5));

            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void CalculateLaserDamage(Ammuninition ammo, EntityControllerBase target, out double damage, out double shieldDamage) {
            if (PlayerController.Locked is PlayerController) { // pvp
                damage = ammo.DamageMultiplier * PlayerController.PlayerHangarAssembly.DamagePVP;
                shieldDamage = ammo.ShieldMultiplier * PlayerController.PlayerHangarAssembly.DamagePVP;
            } else {
                damage = ammo.DamageMultiplier * PlayerController.PlayerHangarAssembly.DamagePVE;
                shieldDamage = ammo.ShieldMultiplier * PlayerController.PlayerHangarAssembly.DamagePVE;
            }

            damage = Noise(damage) * Math.Max(0, target.BoosterAssembly.Get(BoosterType.ENEMY_DAMAGE_LASER)); // normal damage
            shieldDamage = Noise(shieldDamage) * Math.Max(0, target.BoosterAssembly.Get(BoosterType.ENEMY_DAMAGE_LASER)); // sab damage (sab / cbo)
        }

        private bool CheckLaserCount(Ammuninition ammo, bool deduct, out int currentCount) {
            PlayerController.Account.Vault.Ammunitions.TryGetValue(ammo.ID, out currentCount);
            if (ammo.ID == Ammuninition.LCB_10.ID || ammo.ID == Ammuninition.MCB_25.ID) {
                return true; // munitionen, die vom zählen ausgeschlossen werden
            }

            int laserEquippedCount = PlayerController.Account.CurrentHangar.LaserEquippedCount;
            if (currentCount >= laserEquippedCount) {
                if (deduct) {
                    currentCount -= laserEquippedCount;
                    PlayerController.Account.Vault.Ammunitions[ammo.ID] = currentCount;
                }
                return true;
            }
            return false;
        }

        private bool CheckRocketCount(RocketAmmunition rocket, bool deduct, out int currentCount) {
            PlayerController.Account.Vault.RocketAmmunitions.TryGetValue(rocket.ID, out currentCount);
            if (rocket.ID == RocketAmmunition.R310.ID || rocket.ID == RocketAmmunition.PLT_2026.ID) {
                return true; // munitionen, die vom zählen ausgeschlossen werden
            }

            if (currentCount >= 1) {
                if (deduct) {
                    PlayerController.Account.Vault.RocketAmmunitions[rocket.ID] = --currentCount;
                }
                return true;
            }
            return false;
        }


        private void CheckRocketLauncherCount(RocketLauncherAmmunition rlAmmo, int count, out int currentCount) {
            PlayerController.Account.Vault.RocketLauncherAmmunitions.TryGetValue(rlAmmo.ID, out currentCount);

            currentCount -= count;
            PlayerController.Account.Vault.RocketLauncherAmmunitions[rlAmmo.ID] = currentCount;
        }

        private void CalculateHellstormDamage(RocketLauncherAmmunition rlAmmo, int count, out double damage, out double shieldDamage) {
            if (rlAmmo.ID == RocketLauncherAmmunition.UBR_100.ID && !(PlayerController.Locked is PlayerController)) { // pve
                damage = Noise(rlAmmo.Damage * 1.8) * count;
                shieldDamage = Noise(rlAmmo.ShieldDamage * 1.8) * count;
            } else {
                damage = Noise(rlAmmo.Damage) * count; // normal damage
                shieldDamage = Noise(rlAmmo.ShieldDamage) * count; // sab damage (sab / cbo)
            }
        }

        private bool Miss() {
            double hitRate = PlayerController.BoosterAssembly.Get(BoosterType.HIT_RATE) - (PlayerController.Locked.BoosterAssembly.Get(BoosterType.MISS_RATE) - 1);

            if (hitRate <= 0) {
                return true;
            }

            if (hitRate >= 1) {
                return false;
            }

            double rate = random.NextDouble();
            return hitRate < rate;
        }
        #endregion

        #region {[ COUNTER ]}

        #region {[ LASER ]}
        private bool LaserCounter(ref int lapCount) {
            int timeout = 1000;
            lapCount++;
            if (_lastLaserAttack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastLaserAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
            return true;
        }

        private bool LaserRSBCounter(ref int lapCount) {
            const int timeout = 3000;
            lapCount++;
            if (_lastRsbAttack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastRsbAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
            PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(Ammuninition.RSB_75.Name, timeout));
            return true;
        }
        #endregion

        #region {[ ROCKET ]}
        private bool RocketCounter(ref int lapCount) {
            int timeout = (int)(2000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_COOLDOWN));
            lapCount++;
            if (_lastRocketAttack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastRocketAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
            return true;
        }

        private bool RocketPLD8Counter(ref int lapCount) {
            int timeout = (int)(15000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_COOLDOWN));
            lapCount++;
            if (_lastPLD8Attack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastPLD8Attack = PlayerController.CurrentClock.ElapsedMilliseconds;
            PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(RocketAmmunition.PLD_8.Name, timeout));
            return true;
        }

        private bool RocketDCR250Counter(ref int lapCount) {
            int timeout = (int)(15000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_COOLDOWN));
            lapCount++;
            if (_lastDCR250Attack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastDCR250Attack = PlayerController.CurrentClock.ElapsedMilliseconds;
            PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(RocketAmmunition.DCR_250.Name, timeout));
            return true;
        }

        private bool RocketRIC3Counter(ref int lapCount) {
            int timeout = (int)(20000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_COOLDOWN));
            lapCount++;
            if (_lastRIC3Attack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastRIC3Attack = PlayerController.CurrentClock.ElapsedMilliseconds;
            PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(RocketAmmunition.R_IC3.Name, timeout));
            return true;
        }
        #endregion

        private bool RocketLauncherCooldown() {
            int timeout = (int)(4000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_LAUNCHER_COOLDOWN));
            if (_lastRocketLauncherAttack.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                return false;
            }

            return true;
        }

        private bool RocketLauncherRocketCooldown(ref int lapCount) {
            int timeout = (int)(1000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_LAUNCHER_COOLDOWN));
            lapCount++;
            if (_lastRocketLauncherRocketReload.FromNow(PlayerController.CurrentClock.ElapsedMilliseconds) <= timeout) {
                if (lapCount < timeout / 100) {
                    return false;
                }
            }

            lapCount = 1;
            _lastRocketLauncherRocketReload = PlayerController.CurrentClock.ElapsedMilliseconds;
            return true;
        }

        private bool LaserCooldown(ref int lapCount, Ammuninition ammo) {
            if (ammo.ID == Ammuninition.RSB_75.ID) {
                return LaserRSBCounter(ref lapCount);
            }

            return LaserCounter(ref lapCount);
        }
        private bool RocketCooldown(ref int lapCount, RocketAmmunition rocket) {
            if (rocket.ID == RocketAmmunition.PLD_8.ID) {
                return RocketPLD8Counter(ref lapCount);
            } else if (rocket.ID == RocketAmmunition.DCR_250.ID) {
                return RocketDCR250Counter(ref lapCount);
            } else if (rocket.ID == RocketAmmunition.R_IC3.ID) {
                return RocketRIC3Counter(ref lapCount);
            }

            return RocketCounter(ref lapCount);
        }
        #endregion

        #region {[ ATTACKS ]}
        private void ActivateLaserSpecialAttack(Ammuninition ammo) {
            if (ammo == Ammuninition.PIB_100 && PlayerController.Locked.EffectsAssembly is PlayerEffectsAssembly playerEffectsAssembly) {
                playerEffectsAssembly.Infect(15 * 60000);
            }
        }
        public void LaserAttack(ref int lapCount, Ammuninition ammo) {
            lock (_laserLock) {

                #region {[ CHECKING ]}
                if (PlayerController.Locked == null
                    || PlayerController.Locked.MovementAssembly.ActualPosition()
                        .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 600
                    || !_attackRunning) {
                    return;
                }

                bool stopLoop = true;
                bool stopAttack = false;
                if (PlayerController.Locked.ZoneAssembly.IsInDMZ) {
                    PlayerController.Send(PacketBuilder.Messages.PeaceArea());
                    stopAttack = true;
                }

                if (!PlayerController.Account.CurrentHangar.LaserEquipped) {
                    PlayerController.Send(PacketBuilder.Messages.NoLasersOnBoard());
                    stopAttack = true;
                }

                if (!stopAttack && !CheckLaserCount(ammo, false, out int currentCount)) {
                    PlayerController.Send(PacketBuilder.Messages.NoLaserAmmo());
                    stopAttack = true;
                }

                if (!stopAttack && !LaserCooldown(ref lapCount, ammo)) {
                    stopAttack = true;
                    stopLoop = false;
                }

                if (stopAttack) {
                    if (stopLoop) {
                        Stop();
                    }

                    return;
                }
                #endregion

                CheckLaserCount(ammo, true, out currentCount); // deduct

                LastAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
                ICommand attackCommand = PacketBuilder.AttackLaserCommand(PlayerController, PlayerController.Locked, ammo.ID);
                PlayerController.Send(attackCommand, PacketBuilder.Slotbar.LaserItemStatus(ammo.Name, currentCount, true));
                PlayerController.EntitesInRange(x => x.Send(attackCommand));

                if (PlayerController.Locked.EffectsAssembly.HasProtection
                    || (PlayerController.Locked is PlayerController lockedPlayerController && lockedPlayerController.SpecialItemsAssembly.IsInvicible)
                    || Miss()) {
                    PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, 0, 0);
                    ICommand missCommand = new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 0);
                    PlayerController.Locked.Send(new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 1));
                    PlayerController.Locked.EntitiesLocked(x => x.Send(missCommand));
                    return;
                }

                CalculateLaserDamage(ammo, PlayerController.Locked, out double damage, out double shieldDamage);

                if (shieldDamage > 0) {
                    shieldDamage = Math.Abs(PlayerController.Locked.HangarAssembly.ChangeShield(-(int)shieldDamage, false));
                    PlayerController.HangarAssembly.ChangeShield((int)shieldDamage);
                }

                double shieldDamageDealt = shieldDamage;
                double hitpointsDamageDealt = 0;

                if (damage > 0) {
                    double shieldAbsorption = Math.Max(0, Math.Min(1, PlayerController.Locked.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION) - (PlayerController.BoosterAssembly.Get(BoosterType.LASER_SHIELD_PENETRATION) - 1)));

                    double damageShield = damage * shieldAbsorption;
                    if (PlayerController.PlayerAbilityAssembly.WeakenShieldsActive
                        && PlayerController.PlayerAbilityAssembly.WeakenShieldsVictim.ID == PlayerController.Locked.ID) {
                        damageShield *= 1.6;
                    }

                    damageShield = Math.Abs(PlayerController.Locked.HangarAssembly.ChangeShield(-(int)damageShield, false));
                    int damageHitpoints = Math.Abs(PlayerController.Locked.HangarAssembly.ChangeHitpoints(-(int)(damage - Math.Min(damage * shieldAbsorption, damageShield)), false));

                    shieldDamageDealt += damageShield;
                    hitpointsDamageDealt += damageHitpoints;

                    // energy transfer
                    double hitpointsRegain = PlayerController.BoosterAssembly.Get(BoosterType.DAMAGE_LASER_TO_HITPOINTS_TRANSFORMER) - 1.0;
                    if (hitpointsRegain > 0 && ammo.ID == Ammuninition.UCB_100.ID) {
                        int regain = PlayerController.HangarAssembly.ChangeHitpoints(Math.Max(0, (int)((damageShield + damageHitpoints) * hitpointsRegain)));
                        if (regain > 0 && !PlayerController.PlayerTechAssembly.EnergyTransferActive && ++_healCounter % 2 == 0) {
                            _healCounter = 0;

                            ICommand energyTransferCommand = PacketBuilder.AttackLaserCommand(PlayerController.Locked, PlayerController, 7);
                            PlayerController.Locked.Send(energyTransferCommand); // send to player
                            PlayerController.Locked.EntitesInRange(y => y.Send(energyTransferCommand)); // send to all who have him in lock
                        }
                    }

                }

                // set stats
                PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, (int)shieldDamageDealt, (int)hitpointsDamageDealt);
                PlayerController.Locked.HangarAssembly.CheckDeath();

                ActivateLaserSpecialAttack(ammo); // initialize special attack

                if (PlayerController.Locked != null) { // Locked kann nach der CheckDeath-Methode tot sein (wahrscheinlichkeit: hoch)
                    ICommand damageCommand = PacketBuilder.AttackCommand(PlayerController, PlayerController.Locked, AttackTypeModule.LASER, (int)(shieldDamageDealt + hitpointsDamageDealt));
                    PlayerController.Locked.Send(damageCommand); // send to player
                    PlayerController.Locked.EntitiesLocked(y => y.Send(damageCommand)); // send to all who have him in lock
                }
            }
        }

        private void ActivateRocketSpecialAttack(EntityControllerBase target, RocketAmmunition rocket) {
            if (target == null || PlayerController.Locked == null || PlayerController.Locked.ID != target.ID) {
                return;
            }

            if (rocket == RocketAmmunition.DCR_250) { // slow opponent by 30% for 5 seconds
                target.EffectsAssembly.SlowRocket(5000);
            } else if (rocket == RocketAmmunition.PLD_8) {
                target.EffectsAssembly.PLDRocket(5000);
            } else if (rocket == RocketAmmunition.R_IC3) {
                target.EffectsAssembly.IceRocket(3000);
            }
        }
        public void RocketAttack(ref int lapCount, RocketAmmunition rocket, bool force = false) {
            lock (_rocketLock) {

                #region {[ CHECKING ]}
                if (PlayerController.Locked == null
                    || (rocket.ID != RocketAmmunition.R_IC3.ID && PlayerController.Locked.MovementAssembly.ActualPosition()
                        .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 650)
                        || PlayerController.Locked.MovementAssembly.ActualPosition()
                        .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 550
                     || (!force && !_attackRunning)) {
                    return;
                }

                if (PlayerController.Locked.ZoneAssembly.IsInDMZ) {
                    PlayerController.Send(PacketBuilder.Messages.PeaceArea());
                    return;
                }

                if (!CheckRocketCount(rocket, false, out int currentCount)) {
                    PlayerController.Send(PacketBuilder.Messages.NoRocketAmmo());
                    return;
                }

                if (!RocketCooldown(ref lapCount, rocket)) {
                    return;
                }
                #endregion

                CheckRocketCount(rocket, true, out currentCount); // deduct count

                LastAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
                ICommand attackCommand = PacketBuilder.AttackRocketCommand(PlayerController, PlayerController.Locked, rocket.ID);
                PlayerController.Send(attackCommand, PacketBuilder.Slotbar.RocketItemStatus(rocket.Name, currentCount, true));
                PlayerController.EntitesInRange(x => x.Send(attackCommand));

                if (PlayerController.Locked.EffectsAssembly.HasProtection
                    || (PlayerController.Locked is PlayerController lockedPlayerController && lockedPlayerController.SpecialItemsAssembly.IsInvicible)
                    || (!PlayerController.PlayerTechAssembly.PrecisionTargeterActive && Miss())) {
                    PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, 0, 0);
                    ICommand missCommand = new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 0);
                    PlayerController.Locked.Send(new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 1));
                    PlayerController.Locked.EntitiesLocked(x => x.Send(missCommand));
                    return;
                }

                RocketDelayedAttack(PlayerController.Locked, rocket);
            }
        }
        private async void RocketDelayedAttack(EntityControllerBase target, RocketAmmunition rocket) {
            await Task.Delay(500);

            try {
                if (target == null || PlayerController.Locked == null || PlayerController.Locked.ID != target.ID) {
                    return;
                }

                double damage = Noise(rocket.Damage * PlayerController.BoosterAssembly.Get(BoosterType.DAMAGE_ROCKETS));
                if (damage > 0) {
                    double shieldAbsorption = Math.Max(0, Math.Min(1, target.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION)));
                    int damageShield = Math.Abs(target.HangarAssembly.ChangeShield(-(int)(damage * shieldAbsorption), false));
                    int damageHitpoints = Math.Abs(target.HangarAssembly.ChangeHitpoints(-(int)(damage - damageShield), false));

                    // set stats
                    target.AttackTraceAssembly.LogAttack(PlayerController, damageShield, damageHitpoints);
                    target.HangarAssembly.CheckDeath();

                    if (target != null) {
                        ICommand damageCommand = PacketBuilder.AttackCommand(PlayerController, target, AttackTypeModule.ROCKET, (int)damage);
                        target.Send(damageCommand); // send to player
                        target.EntitiesLocked(y => y.Send(damageCommand)); // send to all who have him in lock
                    }
                }

                ActivateRocketSpecialAttack(target, rocket); // initialize special attack
            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
        }

        private void RocketLauncherTimer(ref int lapCount, RocketLauncherAmmunition rlAmmo) {
            lock (_rocketLauncherRocketLock) {
                if (!RocketLauncherCooldown()) {
                    return;
                }

                if (PlayerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount <
                    PlayerController.HangarAssembly.Ship.RocketLauncherSlots * 5) {

                    if (!PlayerController.Account.Vault.RocketLauncherAmmunitions.TryGetValue(PlayerController.Account.CurrentHangar.Selection.RocketLauncher, out int count)
                        || count == PlayerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount) {

                        if (count == 0) { // wenn eine rakete geladen wurde, und ein angriff stattfindet, dann soll diese auch verschossen werden
                            return;
                        }

                    } else {

                        if (!RocketLauncherRocketCooldown(ref lapCount)) {
                            return;
                        }

                        PlayerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount++;
                        PlayerController.Send(PacketBuilder.Slotbar.RocketLauncherStateCommand(PlayerController));
                        return;
                    }
                }

                if (_attackRunning && PlayerController.Account.CurrentHangar.Selection.AutoRocketLauncherCpu) {
                    RocketLauncherAttack(PlayerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount, rlAmmo); // trigger attack
                }
            }
        }
        public void RocketLauncherAttack(int count, RocketLauncherAmmunition rlAmmo) {
            lock (_rocketLauncherLock) {

                #region {[ CHECKING ]}
                if (PlayerController.Locked == null
                    || PlayerController.Locked.MovementAssembly.ActualPosition()
                        .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 650
                    || count <= 0) {
                    return;
                }

                if (PlayerController.Locked.ZoneAssembly.IsInDMZ) {
                    PlayerController.Send(PacketBuilder.Messages.PeaceArea());
                    return;
                }

                if (!RocketLauncherCooldown()) {
                    return;
                }

                CheckRocketLauncherCount(rlAmmo, count, out int currentCount);
                #endregion

                LastAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
                _lastRocketLauncherAttack = PlayerController.CurrentClock.ElapsedMilliseconds;
                PlayerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount = 0;

                ICommand attackCommand = PacketBuilder.AttackHellstormCommand(PlayerController, PlayerController.Locked, count, (short)rlAmmo.ID);
                PlayerController.Send(
                    attackCommand,
                    PacketBuilder.Slotbar.RocketLauncherStateCommand(PlayerController),
                    PacketBuilder.Slotbar.ItemCooldownCommand("equipment_weapon_rocketlauncher_hst", (int)(3000 * PlayerController.BoosterAssembly.Get(BoosterType.ROCKET_LAUNCHER_COOLDOWN))),
                    PacketBuilder.Slotbar.RocketItemStatus(rlAmmo.Name, currentCount, false)
                );
                PlayerController.EntitesInRange(x => x.Send(attackCommand));

                int hitCount = 0;
                for (int i = 0; i < count; i++) {
                    if (!Miss()) {
                        hitCount++;
                    }
                }

                if (PlayerController.Locked.EffectsAssembly.HasProtection
                    || (rlAmmo.ID != RocketLauncherAmmunition.HSTRM_01.ID
                        && PlayerController.Locked is PlayerController lockedPlayerController
                        && lockedPlayerController.SpecialItemsAssembly.IsInvicible)
                    || hitCount == 0) {
                    PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, 0, 0);
                    ICommand missCommand = new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 0);
                    PlayerController.Locked.Send(new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.LASER), PlayerController.Locked.ID, 1));
                    PlayerController.Locked.EntitiesLocked(x => x.Send(missCommand));
                    return;
                }

                RocketLauncherDelayedAttack(PlayerController.Locked, rlAmmo, hitCount);
            }
        }

        private async void RocketLauncherDelayedAttack(EntityControllerBase target, RocketLauncherAmmunition rlAmmo, int hitCount) {
            await Task.Delay(750);

            try {
                if (target == null || PlayerController.Locked == null || PlayerController.Locked.ID != target.ID) {
                    return;
                }

                CalculateHellstormDamage(rlAmmo, hitCount, out double damage, out double shieldDamage);

                if (shieldDamage > 0) {
                    shieldDamage = Math.Abs(target.HangarAssembly.ChangeShield(-(int)shieldDamage, false));
                    PlayerController.HangarAssembly.ChangeShield((int)shieldDamage);
                }

                double shieldDamageDealt = shieldDamage;
                double hitpointsDamageDealt = 0;

                if (damage > 0) {
                    double shieldAbsorption = Math.Max(0, Math.Min(1, target.BoosterAssembly.Get(BoosterType.SHIELD_ABSORBATION)));
                    int damageShield = Math.Abs(target.HangarAssembly.ChangeShield(-(int)(damage * shieldAbsorption), false));
                    int damageHitpoints = Math.Abs(target.HangarAssembly.ChangeHitpoints(-(int)(damage - damageShield), false));

                    shieldDamageDealt += damageShield;
                    hitpointsDamageDealt += damageHitpoints;
                }

                // set stats
                target.AttackTraceAssembly.LogAttack(PlayerController, (int)shieldDamageDealt, (int)hitpointsDamageDealt);
                target.HangarAssembly.CheckDeath();

                if (target != null) {
                    ICommand damageCommand = PacketBuilder.AttackCommand(PlayerController, target, AttackTypeModule.ROCKET, (int)(shieldDamage + damage));
                    target.Send(damageCommand); // send to player
                    target.EntitiesLocked(y => y.Send(damageCommand)); // send to all who have him in lock
                }
            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
        }

        protected override void AttackTimer() {
            if (PlayerController.Locked == null) {
                Stop();
                return;
            }

            if (PlayerController.Locked.ZoneAssembly.IsInDMZ) {
                PlayerController.Send(PacketBuilder.Messages.PeaceArea());
                Stop();
                return;
            }

            if (PlayerController.MovementAssembly.ActualPosition().DistanceTo(PlayerController.Locked.MovementAssembly.ActualPosition()) > 650) {
                if (_outOfRangeCounter++ % 10 == 0) {
                    PlayerController.Send(PacketBuilder.Messages.OutOfRange());
                    PlayerController.Locked.Send(PacketBuilder.Messages.AttackEscaped());
                }

                _targetOutOfRange = true;
            } else {
                _targetOutOfRange = false;

                if (_attackTimer.Timeout != 100) {
                    _attackTimer.Timeout = 100;
                    _outOfRangeCounter = 0;
                }

                if (!_attackRunning) {
                    return;
                }

                try {
                    LaserAttack(ref _lapsSinceLastLaser, PlayerController.Account.CurrentHangar.Selection.Laser.FromAmmunitions());

                    if (PlayerController.Account.CurrentHangar.Selection.AutoRocketCpu) {
                        RocketAttack(ref _lapsSinceLastRocket, PlayerController.Account.CurrentHangar.Selection.Rocket.FromRocketAmmunitions());
                    }
                } catch { }
            }
        }
        private void RocketLauncherReloadTimer() {
            if (PlayerController.Account.CurrentHangar.Selection.RocketLauncher != 0) { // xd
                try {
                    RocketLauncherTimer(ref _lapsSinceLastRocketLauncherRocket,
                    PlayerController.Account.CurrentHangar.Selection.RocketLauncher.FromRocketLauncherAmmunitions());
                } catch { }
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Start() {
            _outOfRangeCounter = 0;
            if (Controller.Locked != null) {
                _attackRunning = true;
                _targetOutOfRange = false;
            }
        }

        public override void Stop() {
            PlayerController.Send(new AbortDirectionLockCommand(PlayerController.ID));
            if (_attackRunning) {
                _attackRunning = false;
                _targetOutOfRange = false;

                _attackTimer.Reset(100, 100); // only laser do attack instantly after opponent change
            }
        }

        public void Reset() {
            _lastLaserAttack = 0; // reset time
        }
        #endregion

        #region {[ TIMING ]}
        protected override void Tick(double changeSinceLastTime) {
            _rocketLauncherReloadTimer.Tick(changeSinceLastTime); // always reload and only attack when _attackRunning!

            if (_attackRunning) {
                _attackTimer.Tick(changeSinceLastTime);
            }
        }
        #endregion

    }
}
