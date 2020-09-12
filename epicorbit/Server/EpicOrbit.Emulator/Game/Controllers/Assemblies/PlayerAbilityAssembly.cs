using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerAbilityAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; protected set; }

        public EntityControllerBase WeakenShieldsVictim => _weakenShieldsVictim;
        public bool WeakenShieldsActive => _weakenShieldsActive;
        #endregion

        #region {[ FIELDS ]}
        private TickInterval _checkTickInterval;
        private EntityControllerBase _weakenShieldsVictim;
        private int _singularityCounter, _singularityLastDamage, _singularityDamageIncrease;
        private bool _fortessActive, _prismaticShieldActive, _afterBurnerActive, _singularityActive, _weakenShieldsActive;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerAbilityAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
            Initialize();

            _checkTickInterval = new TickInterval(Check, 500);
            PlayerController.OnTick += _checkTickInterval.Tick;
        }

        private void Initialize() {
            if (IsActive(ShipAbility.AFTERBURNER)) {
                _afterBurnerActive = true;
                PlayerController.BoosterAssembly.Multiply(BoosterType.SPEED, 1.3);
            }

            if (IsActive(ShipAbility.FORTRESS)) {
                _fortessActive = true;
                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_SHIELD, 0.7);
            }

            if (IsActive(ShipAbility.PRISMATIC_SHIELD)) {
                _prismaticShieldActive = true;
                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_LASER, 0.55);
                PlayerController.BoosterAssembly.Multiply(BoosterType.ENEMY_DAMAGE_LASER, 0.3);
            }
        }
        #endregion

        #region {[ HELPER ]}
        private bool IsActive(ShipAbility ability) {
            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            return lastAbilityTime.FromNow() < ability.Duration;
        }
        #endregion

        #region {[ TIMING ]}
        private void Check() {
            CheckOrStopFortress();
            CheckOrStopPrismaticShield();
            CheckOrStopAfterburner();
            CheckOrStopSingularity();
            CheckOrStopWeakenShields();
        }
        #endregion

        #region {[ SINGULARITY ]}
        public void StartSingularity(int initialDamage, int incrementialDamage) {
            ShipAbility ability = ShipAbility.SINGULARITY;

            if (PlayerController.Locked == null
                || PlayerController.Locked.MovementAssembly.ActualPosition()
                    .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 600) {
                return;
            }

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (!_singularityActive && lastAbilityTime.FromNow() > ability.Duration + ability.Cooldown) {
                _singularityActive = true;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                PlayerController.EffectsAssembly.ActivateSingularity();
                PlayerController.Locked.EffectsAssembly.ActivateSingularity();

                _singularityLastDamage = initialDamage;
                _singularityDamageIncrease = incrementialDamage;
                _singularityCounter = 1;

                PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Duration.TotalMilliseconds));
            }
        }

        public void CheckOrStopSingularity(bool force = false) {
            try {
                ShipAbility ability = ShipAbility.SINGULARITY;

                PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
                if (_singularityActive && (_singularityCounter == (int)ability.Duration.TotalSeconds * 2 || force)) {
                    _singularityActive = false;
                    PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now - ability.Duration;

                    PlayerController.EffectsAssembly.DeactivateSingularity();
                    PlayerController.Locked.EffectsAssembly.DeactivateSingularity();

                    PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                } else if (_singularityActive) {
                    if (++_singularityCounter % 2 == 0) {

                        if (PlayerController.Locked.EffectsAssembly.HasProtection
                            || (PlayerController.Locked is PlayerController lockedPlayerController
                                && lockedPlayerController.SpecialItemsAssembly.IsInvicible)) {

                            PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, 0, 0, false);
                            ICommand missCommand = new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.SINGULARITY), PlayerController.Locked.ID, 0);
                            PlayerController.Locked.Send(new AttackMissedCommand(new AttackTypeModule(AttackTypeModule.SINGULARITY), PlayerController.Locked.ID, 1));
                            PlayerController.Locked.EntitiesLocked(x => x.Send(missCommand));

                        } else {

                            PlayerController.Locked.HangarAssembly.ChangeHitpoints(-_singularityLastDamage, false);
                            PlayerController.Locked.AttackTraceAssembly.LogAttack(PlayerController, 0, _singularityLastDamage, false);
                            PlayerController.Locked.HangarAssembly.CheckDeath();

                            ICommand damageCommand = PacketBuilder.AttackCommand(PlayerController, PlayerController.Locked, AttackTypeModule.SINGULARITY, _singularityLastDamage);
                            PlayerController.Locked.Send(damageCommand); // send to player
                            PlayerController.Locked.EntitiesLocked(y => y.Send(damageCommand)); // send to all who have him in lock

                        }

                        _singularityLastDamage += _singularityDamageIncrease;
                    }
                }
            } catch { }
        }
        #endregion

        #region {[ FORTRESS ]}
        public void StartFortress() {
            ShipAbility ability = ShipAbility.FORTRESS;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (!_fortessActive && lastAbilityTime.FromNow() > ability.Duration + ability.Cooldown) {
                _fortessActive = true;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_SHIELD, 0.7);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 15, true);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Duration.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }

        public void CheckOrStopFortress(bool force = false) {
            ShipAbility ability = ShipAbility.FORTRESS;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (_fortessActive && (lastAbilityTime.FromNow() > ability.Duration || force)) {
                _fortessActive = false;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now - ability.Duration;

                PlayerController.BoosterAssembly.Divide(BoosterType.DAMAGE_SHIELD, 0.7);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 15, false);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }
        #endregion

        #region {[ WEAKEN SHIELDS ]}
        public void StartWeakenShields() {
            ShipAbility ability = ShipAbility.WEAKEN_SHIELDS;

            if (PlayerController.Locked == null
                || PlayerController.Locked.MovementAssembly.ActualPosition()
                    .DistanceTo(PlayerController.MovementAssembly.ActualPosition()) > 600) {
                return;
            }

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (!_weakenShieldsActive && lastAbilityTime.FromNow() > ability.Duration + ability.Cooldown) {
                _weakenShieldsActive = true;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                _weakenShieldsVictim = PlayerController.Locked;

                PlayerController.EffectsAssembly.ActivateWeakenShields();
                _weakenShieldsVictim.EffectsAssembly.ActivateWeakenShields();



                PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Duration.TotalMilliseconds));
            }
        }

        public void CheckOrStopWeakenShields(bool force = false) {
            try {
                ShipAbility ability = ShipAbility.WEAKEN_SHIELDS;

                PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
                if (_weakenShieldsActive && (lastAbilityTime.FromNow() > ability.Duration || force)) {
                    _weakenShieldsActive = false;
                    PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now - ability.Duration;

                    PlayerController.EffectsAssembly.DeactivateWeakenShields();
                    _weakenShieldsVictim.EffectsAssembly.DeactivateWeakenShields();
                    _weakenShieldsVictim = null;

                    PlayerController.HangarAssembly.ChangeShield(-(int)(PlayerController.HangarAssembly.Shield * 0.10));
                    PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                }
            } catch (Exception e) {
                GameContext.Logger.LogError(e);
            }
        }
        #endregion

        #region {[ NANO-CLUSTER REPAIR ]}
        public void StartNanoClusterRepair() {
            ShipAbility ability = ShipAbility.NANO_CLUSTER_REPAIR;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (lastAbilityTime.FromNow() > ability.Cooldown) {
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                PlayerController.HangarAssembly.ChangeHitpoints((int)(PlayerController.HangarAssembly.MaxHitpoints * 0.35));

                ICommand effectCommand = PacketBuilder.Legacy($"0|n|SD|A|1|{PlayerController.ID}"); // mit |<ID>|... können mehrere ID's angehängt werden (Später für Gruppensystem)
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }
        #endregion

        #region {[ PRISMATIC SHIELD ]}
        public void StartPrismaticShield() {
            ShipAbility ability = ShipAbility.PRISMATIC_SHIELD;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (!_prismaticShieldActive && lastAbilityTime.FromNow() > ability.Duration + ability.Cooldown) {
                _prismaticShieldActive = true;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_LASER, 0.55);
                PlayerController.BoosterAssembly.Multiply(BoosterType.ENEMY_DAMAGE_LASER, 0.3);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 16, true);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Duration.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }

        public void CheckOrStopPrismaticShield(bool force = false) {
            ShipAbility ability = ShipAbility.PRISMATIC_SHIELD;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (_prismaticShieldActive && (lastAbilityTime.FromNow() > ability.Duration || force)) {
                _prismaticShieldActive = false;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now - ability.Duration;

                PlayerController.BoosterAssembly.Divide(BoosterType.DAMAGE_LASER, 0.55);
                PlayerController.BoosterAssembly.Divide(BoosterType.ENEMY_DAMAGE_LASER, 0.3);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 16, false);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }
        #endregion

        #region {[ AFTERBURNER ]}
        public void StartAfterburner() {
            ShipAbility ability = ShipAbility.AFTERBURNER;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (!_afterBurnerActive && lastAbilityTime.FromNow() > ability.Duration + ability.Cooldown) {
                _afterBurnerActive = true;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now;

                PlayerController.BoosterAssembly.Multiply(BoosterType.SPEED, 1.3);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 0, true);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Duration.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }

        public void CheckOrStopAfterburner(bool force = false) {
            ShipAbility ability = ShipAbility.AFTERBURNER;

            PlayerController.Account.Cooldown.AbilityCooldown.TryGetValue(ability.ID, out DateTime lastAbilityTime);
            if (_afterBurnerActive && (lastAbilityTime.FromNow() > ability.Duration || force)) {
                _afterBurnerActive = false;
                PlayerController.Account.Cooldown.AbilityCooldown[ability.ID] = DateTime.Now - ability.Duration;

                PlayerController.BoosterAssembly.Divide(BoosterType.SPEED, 1.3);

                ICommand effectCommand = PacketBuilder.VisualModifier(PlayerController, 0, false);
                PlayerController.Send(effectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(ability.Name, ability.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(effectCommand));
            }
        }
        #endregion

        #region {[ HP REPAIR ]}
        #endregion

        #region {[ JAMX ]}
        #endregion

        #region {[ PROTECTION ]}
        #endregion

        #region {[ RAPID FIRE ]}
        #endregion

        #region {[ RECON ]}
        #endregion

        #region {[ REPAIR POD ]}
        #endregion

        #region {[ SHIELD REPAIR ]}
        #endregion

        #region {[ SPEED BOOST ]}
        #endregion

        #region {[ TARGET MARKER ]}
        #endregion

        #region {[ TRAVEL ]}
        #endregion

        #region {[ ULTIMATE CLOAKING ]}
        #endregion

        #region {[ FUNCTIONS ]}
        public IEnumerable<ICommand> EffectsCommand() {
            if (_fortessActive) {
                yield return PacketBuilder.VisualModifier(PlayerController, 15, true);
            }

            if (_prismaticShieldActive) {
                yield return PacketBuilder.VisualModifier(PlayerController, 16, true);
            }

            if (_afterBurnerActive) {
                yield return PacketBuilder.VisualModifier(PlayerController, 0, true);
            }
        }

        public override void Refresh() {
            PlayerController.Send(EffectsCommand());
        }
        #endregion

    }
}
