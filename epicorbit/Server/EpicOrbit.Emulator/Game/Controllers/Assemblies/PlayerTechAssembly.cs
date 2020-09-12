using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerTechAssembly : AssemblyBase {

        #region {[ STATIC ]}
        protected static Random random = new Random();
        #endregion

        #region {[ PROPERTIES ]}
        public PlayerController PlayerController { get; set; }

        public bool PrecisionTargeterActive => _rpmActive;
        public bool EnergyTransferActive => _elaActive;
        #endregion

        #region {[ FIELDS ]}
        private TickInterval _checkTickInterval;

        private int _brbLapCounter;
        private bool _elaActive, _brbActive, _rpmActive;
        private DateTime _lastELA, _lastSBU, _lastBRB, _lastRPM, _lastCI;
        #endregion

        #region {[ CONSTRUCTOR & INITIALIZER ]}
        public PlayerTechAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
            Initialize();

            _checkTickInterval = new TickInterval(Check, 100);
            PlayerController.OnTick += _checkTickInterval.Tick;
        }

        private void Initialize() {
            PlayerController.Account.Cooldown.TechCooldown.TryGetValue(TechFactory.ENERGY_LEECH.ID, out _lastELA);
            PlayerController.Account.Cooldown.TechCooldown.TryGetValue(TechFactory.BATTLE_REPAIR_BOT.ID, out _lastBRB);
            PlayerController.Account.Cooldown.TechCooldown.TryGetValue(TechFactory.CHAIN_IMPULSE.ID, out _lastCI);
            PlayerController.Account.Cooldown.TechCooldown.TryGetValue(TechFactory.PRECISION_TARGETER.ID, out _lastRPM);
            PlayerController.Account.Cooldown.TechCooldown.TryGetValue(TechFactory.SHIELD_BACKUP.ID, out _lastSBU);

            if (_lastELA.FromNow() < TechFactory.ENERGY_LEECH.Duration) {
                _elaActive = true;
                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_LASER_TO_HITPOINTS_TRANSFORMER, 1.1);
            }

            _rpmActive = _lastRPM.FromNow() < TechFactory.PRECISION_TARGETER.Duration;
            _brbActive = _lastBRB.FromNow() < TechFactory.BATTLE_REPAIR_BOT.Duration;
        }
        #endregion

        #region {[ TIMING ]}
        private void Check() {
            CheckEnergyTransfer();
            CheckBattleRepairBot();
            CheckPrecisionTargeter();
        }
        #endregion

        #region {[ ENERGY TRANSFER ]}
        public void StartEnergyTransfer() {
            TechFactory tech = TechFactory.ENERGY_LEECH;
            if (!_elaActive && _lastELA.FromNow() > tech.Duration + tech.Cooldown) {

                if (!PlayerController.Account.Vault.Techs.TryGetValue(tech.ID, out int currentCount)
                || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Techs[tech.ID] = --currentCount;

                _elaActive = true;
                _lastELA = DateTime.Now;

                PlayerController.Account.Cooldown.TechCooldown[tech.ID] = _lastELA;
                PlayerController.BoosterAssembly.Multiply(BoosterType.DAMAGE_LASER_TO_HITPOINTS_TRANSFORMER, 1.1);

                ICommand showEffectCommand = PacketBuilder.VisualModifier(PlayerController, 58, true);
                PlayerController.Send(
                    showEffectCommand,
                    PacketBuilder.Slotbar.TechItemStatus(tech.Name, currentCount, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Duration.TotalMilliseconds)
                );
                PlayerController.EntitesInRange(x => x.Send(showEffectCommand));
            }
        }

        private void CheckEnergyTransfer() {
            TechFactory tech = TechFactory.ENERGY_LEECH;
            if (_elaActive && _lastELA.FromNow() > tech.Duration) {
                _elaActive = false;
                PlayerController.BoosterAssembly.Divide(BoosterType.DAMAGE_LASER_TO_HITPOINTS_TRANSFORMER, 1.1);

                ICommand removeEffectCommand = PacketBuilder.VisualModifier(PlayerController, 58, false);
                PlayerController.Send(removeEffectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(removeEffectCommand));
            }
        }
        #endregion

        #region {[ BATTLE REPAIR ROBOT ]}
        public void StartBattleRepairBot() {
            TechFactory tech = TechFactory.BATTLE_REPAIR_BOT;
            if (!_brbActive && _lastBRB.FromNow() > tech.Duration + tech.Cooldown) {

                if (!PlayerController.Account.Vault.Techs.TryGetValue(tech.ID, out int currentCount)
                 || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Techs[tech.ID] = --currentCount;

                _brbActive = true;
                _lastBRB = DateTime.Now;

                PlayerController.Account.Cooldown.TechCooldown[tech.ID] = _lastBRB;
                _brbLapCounter = 10;

                PlayerController.HangarAssembly.ChangeHitpoints(10000, force: true);

                ICommand showEffectCommand = PacketBuilder.VisualModifier(PlayerController, 59, true);
                PlayerController.Send(showEffectCommand,
                    PacketBuilder.Slotbar.TechItemStatus(tech.Name, currentCount, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Duration.TotalMilliseconds)
                );
                PlayerController.EntitesInRange(x => x.Send(showEffectCommand));
            }
        }

        private void CheckBattleRepairBot() {
            TechFactory tech = TechFactory.BATTLE_REPAIR_BOT;
            if (_brbActive && _brbLapCounter == 101) {

                _brbActive = false;

                ICommand removeEffectCommand = PacketBuilder.VisualModifier(PlayerController, 59, false);
                PlayerController.Send(removeEffectCommand, PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Cooldown.TotalMilliseconds));
                PlayerController.EntitesInRange(x => x.Send(removeEffectCommand));
            } else if (_brbActive) {
                if (++_brbLapCounter % 10 == 0) {
                    PlayerController.HangarAssembly.ChangeHitpoints(10000, force: true);
                }
            }
        }
        #endregion

        #region {[ SHIELD BACKUP ]}
        public void StartShieldBackup() {
            TechFactory tech = TechFactory.SHIELD_BACKUP;
            if (_lastSBU.FromNow() > tech.Cooldown) {

                if (!PlayerController.Account.Vault.Techs.TryGetValue(tech.ID, out int currentCount)
                || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Techs[tech.ID] = --currentCount;

                _lastSBU = DateTime.Now;
                PlayerController.Account.Cooldown.TechCooldown[tech.ID] = _lastSBU;

                PlayerController.HangarAssembly.ChangeShield(75000, displayChange: true);

                if (PlayerController.Spacemap != null) {
                    foreach (var other in PlayerController.Spacemap.EntitiesOnMap(PlayerController).ToList()) {
                        if (other is PlayerController playerController
                            && playerController.PlayerAbilityAssembly.WeakenShieldsActive
                            && playerController.PlayerAbilityAssembly.WeakenShieldsVictim.ID == PlayerController.ID) {
                            playerController.PlayerAbilityAssembly.CheckOrStopWeakenShields(true);
                        }
                    }
                }

                ICommand showEffectCommand = PacketBuilder.SendTechEffect(PlayerController, "SBU");
                PlayerController.Send(
                    showEffectCommand,
                    PacketBuilder.Slotbar.TechItemStatus(tech.Name, currentCount, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Cooldown.TotalMilliseconds)
                );
                PlayerController.EntitesInRange(x => x.Send(showEffectCommand));
            }
        }
        #endregion

        #region {[ PRECISION TARGETER ]}
        public void StartPrecisionTargeter() {
            TechFactory tech = TechFactory.PRECISION_TARGETER;
            if (!_rpmActive && _lastRPM.FromNow() > tech.Duration + tech.Cooldown) {

                if (!PlayerController.Account.Vault.Techs.TryGetValue(tech.ID, out int currentCount)
                || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Techs[tech.ID] = --currentCount;

                _rpmActive = true;
                _lastRPM = DateTime.Now;

                PlayerController.Account.Cooldown.TechCooldown[tech.ID] = _lastRPM;
                PlayerController.Send(
                    PacketBuilder.Slotbar.TechItemStatus(tech.Name, currentCount, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Duration.TotalMilliseconds)
                );
            }
        }

        private void CheckPrecisionTargeter() {
            TechFactory tech = TechFactory.PRECISION_TARGETER;
            if (_rpmActive && _lastRPM.FromNow() > tech.Duration) {
                _rpmActive = false;

                PlayerController.Send(PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Cooldown.TotalMilliseconds));
            }
        }
        #endregion

        #region {[ CHAIN IMPULSE ]}
        private double Noise(double value, double percent = .2) {
            double minimum = value - (value * percent);
            double maximum = value;

            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public void StartChainImpulse() {
            TechFactory tech = TechFactory.CHAIN_IMPULSE;
            if (_lastCI.FromNow() > tech.Cooldown) {

                if (!PlayerController.Account.Vault.Techs.TryGetValue(tech.ID, out int currentCount)
                || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Techs[tech.ID] = --currentCount;

                bool working = false;
                double toDeal = Noise(10000);
                StringBuilder chainCommand = new StringBuilder($"0|TX|ECI||{PlayerController.ID}");
                foreach (EntityControllerBase entity in
                    PlayerController.Spacemap.InRange(PlayerController, 1000)
                    .OrderBy(x => x.MovementAssembly.ActualPosition().DistanceTo(PlayerController.MovementAssembly.ActualPosition()))
                    .Take(7)) {
                    working = true;
                    chainCommand.Append($"|{entity.ID}");

                    int damage = Math.Abs(entity.HangarAssembly.ChangeHitpoints(-(int)toDeal, false));
                    toDeal = Noise(toDeal);

                    entity.AttackTraceAssembly.LogAttack(PlayerController, 0, damage, false);
                    entity.HangarAssembly.CheckDeath();

                    ICommand damageCommand = PacketBuilder.AttackCommand(PlayerController, entity, AttackTypeModule.LASER, damage);
                    entity.Send(damageCommand); // send to player
                    PlayerController.Send(damageCommand); // send to player
                    entity.EntitiesLocked(y => {
                        if (y.ID == PlayerController.ID) {
                            return;
                        }

                        y.Send(damageCommand);
                    }); // send to all who have him in lock
                }

                if (!working) {
                    return;
                } else {
                    _lastCI = DateTime.Now;
                    PlayerController.Account.Cooldown.TechCooldown[tech.ID] = _lastCI;
                }

                ICommand showEffectCommand = PacketBuilder.Legacy(chainCommand.ToString());
                PlayerController.Send(
                    showEffectCommand,
                    PacketBuilder.Slotbar.TechItemStatus(tech.Name, currentCount, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(tech.Name, tech.Cooldown.TotalMilliseconds)
                );
                PlayerController.EntitesInRange(x => x.Send(showEffectCommand));
            }
        }
        #endregion

        public virtual IEnumerable<ICommand> EffectsCommand() {
            if (_elaActive) {
                yield return PacketBuilder.VisualModifier(PlayerController, 58, true);
            }

            if (_brbActive) {
                yield return PacketBuilder.VisualModifier(PlayerController, 59, true);
            }
        }

        public override void Refresh() {
            Controller.Send(EffectsCommand());
        }
    }
}
