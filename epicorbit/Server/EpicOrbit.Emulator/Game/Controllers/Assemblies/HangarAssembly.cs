using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Emulator.Game.Implementations;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Server.Data.Models.Modules;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items;
using System;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class HangarAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public virtual Ship Ship { get; set; }
        public virtual Map Map { get; set; }
        public virtual Position Position { get; set; }

        public int MaxHitpoints => (int)(_maxHitpoints * Controller.BoosterAssembly.Get(BoosterType.HITPOINTS));
        protected virtual int _maxHitpoints { get; }

        public int Hitpoints {
            get => Math.Min(_hitpoints, MaxHitpoints);
            protected set => _hitpoints = value;
        }
        protected virtual int _hitpoints { get; set; }

        public int MaxShield => (int)(_maxShield * Controller.BoosterAssembly.Get(BoosterType.SHIELD));
        protected virtual int _maxShield { get; }

        public int Shield {
            get => Math.Min(_shield, MaxShield);
            protected set => _shield = value;
        }
        protected virtual int _shield { get; set; }

        public int Speed => (int)(_speed * Controller.BoosterAssembly.Get(BoosterType.SPEED));
        protected virtual int _speed { get; }

        public int Damage => (int)(_damage * Controller.BoosterAssembly.Get(BoosterType.DAMAGE) * Controller.BoosterAssembly.Get(BoosterType.DAMAGE_LASER));
        protected virtual int _damage { get; }
        protected virtual int SelectedAmmo { get; }
        #endregion

        #region {[ FIELDS ]}
        private bool _isRegenerating, _showRepairBot;
        private TickInterval _regenerateTickFunction;
        private TickInterval _checkAttackTimeFunction;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public HangarAssembly(EntityControllerBase controller) : base(controller) {
            _regenerateTickFunction = new TickInterval(Regenerate, 1000);
            _checkAttackTimeFunction = new TickInterval(CheckLastAttacked, 333);

            controller.BoosterAssembly.OnBoostChanged += BoosterAssembly_BoostChanged;
            controller.OnTick += Tick;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public int ChangeHitpoints(int change, bool broadcast = true, bool displayChange = true, bool force = false) {
            change = Math.Max(-Hitpoints, Math.Min(MaxHitpoints - Hitpoints, change));
            Hitpoints += change;

            if (change != 0 && broadcast || force) {
                if (displayChange) {
                    ICommand command = PacketBuilder.HitpointsChangeDisplayedCommand(Controller, change);

                    Controller.Send(command);
                    Controller.EntitiesLocked(x => x.Send(command));
                } else {
                    Controller.Send(PacketBuilder.HitpointsChangeCommand(Controller));
                    Controller.EntitiesLocked(x => x.Send(PacketBuilder.TargetHealthChangeCommand(x, Controller)));
                }

                CheckDeath();
            }

            if (change > 0) {
                Controller.AttackTraceAssembly.HitpointsRepair(change);
            }

            return change;
        }

        public void CheckDeath() {
            if (Hitpoints <= 0) {
                Controller.Die();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="change"></param>
        /// <param name="broadcast"></param>
        /// <param name="displayChange">shield does not need to be displayed</param>
        /// <returns></returns>
        public int ChangeShield(int change, bool broadcast = true, bool displayChange = false, bool force = false, bool ignoreEffects = false) {
            change = Math.Max(-Shield, Math.Min(MaxShield - Shield, change));

            if (!ignoreEffects && change < 0) {
                change = (int)(change * Controller.BoosterAssembly.Get(BoosterType.DAMAGE_SHIELD));
            }

            Shield += change;

            if (change != 0 && broadcast || force) {
                if (displayChange) {
                    ICommand command = PacketBuilder.ShieldChangeDisplayedCommand(Controller, change);

                    Controller.Send(command);
                    Controller.EntitiesLocked(x => x.Send(command));
                } else {
                    Controller.Send(PacketBuilder.ShieldChangeCommand(Controller));
                    Controller.EntitiesLocked(x => x.Send(PacketBuilder.TargetHealthChangeCommand(x, Controller)));
                }
            }

            if (change > 0) {
                Controller.AttackTraceAssembly.ShieldRepair(change);
            }

            return change;

            /*     int supposedChange = Math.Max(-Shield, Math.Min(MaxShield - Shield, change));

            change = supposedChange;

            if (!ignoreEffects && change < 0) {
                change = (int)(change * Controller.BoosterAssembly.Get(BoosterType.DAMAGE_SHIELD));
            }

            Shield += change;

            if (change != 0 && broadcast || force) {
                if (displayChange) {
                    ICommand command = PacketBuilder.ShieldChangeDisplayedCommand(Controller, change);

                    Controller.Send(command);
                    Controller.EntitiesLocked(x => x.Send(command));
                } else {
                    Controller.Send(PacketBuilder.ShieldChangeCommand(Controller));
                    Controller.EntitiesLocked(x => x.Send(PacketBuilder.TargetHealthChangeCommand(x, Controller)));
                }
            }

            if (change > 0) {
                Controller.AttackTraceAssembly.ShieldRepair(change);
            }

            return supposedChange; */
        }

        public override void Refresh() { }
        #endregion

        #region {[ EVENTHANDLER ]}
        private void Regenerate() {
            if (Controller.EffectsAssembly.HasProtection || Controller.CurrentClock.ElapsedMilliseconds - Controller.AttackTraceAssembly.LastAttackTime > 10000) {

                if ((!Controller.AttackAssembly.AttackRunning || Controller.AttackAssembly.TargetOutOfRange)
                    && Controller.CurrentClock.ElapsedMilliseconds - Controller.AttackAssembly.LastAttack > 2000
                    && Hitpoints < MaxHitpoints) {

                    if (ChangeHitpoints((int)(MaxHitpoints * Controller.BoosterAssembly.Get(BoosterType.HITPOINTS_REGENERATION))) > 0 && !_showRepairBot) {
                        _showRepairBot = true;
                        Controller.ZoneAssembly.ShowRepairRobot();
                    }

                } else if (_showRepairBot) {
                    _showRepairBot = false;
                    Controller.ZoneAssembly.HideRepairBot();
                }

                if (Controller is PlayerController controller) {
                    if (controller.DroneFormationAssembly.DroneFormation.ID == DroneFormation.MOTH_ID
                          || controller.DroneFormationAssembly.DroneFormation.ID == DroneFormation.WHEEL_ID) {
                        return; // do not regenrate shield while moth or wheel is active
                    }
                }

                if (Shield < MaxShield) {
                    ChangeShield((int)(MaxShield * Controller.BoosterAssembly.Get(BoosterType.SHIELD_REGNERATION)));
                }

            } else {
                _isRegenerating = false;
                _regenerateTickFunction.Reset();
            }
        }

        private void CheckLastAttacked() {
            if (Controller.EffectsAssembly.HasProtection || Controller.CurrentClock.ElapsedMilliseconds - Controller.AttackTraceAssembly.LastAttackTime > 5000) {
                _isRegenerating = true;
                _checkAttackTimeFunction.Reset();
            } else if (_showRepairBot) {
                _showRepairBot = false;
                Controller.ZoneAssembly.HideRepairBot();
            }
        }

        private void Tick(double timeSinceLastChange) {
            if (!_isRegenerating) {
                _checkAttackTimeFunction.Tick(timeSinceLastChange);
            }

            if (_isRegenerating) {
                _regenerateTickFunction.Tick(timeSinceLastChange);
            }
        }

        private void BoosterAssembly_BoostChanged(BoosterType boosterType, double newValue) {
            switch (boosterType) {
                case BoosterType.HITPOINTS:
                    BroadcastHealth();
                    break;
                case BoosterType.SHIELD:
                    BroadcastShield();
                    break;
                case BoosterType.SPEED:
                    BroadcastSpeed();
                    if (Controller.MovementAssembly != null) {
                        Controller.MovementAssembly.RefreshMovement();
                    }
                    break;
            }
        }

        public void BroadcastHealth() {

            Hitpoints = Hitpoints; // update hp / this is useful dont fuck with this line amk / glaub du hast vergessen wozu diese line
            Controller.Send(PacketBuilder.HitpointsChangeCommand(Controller));

            ICommand hpChangedCommand = PacketBuilder.TargetHealthBaseChangedCommand(Controller);
            Controller.EntitiesLocked(x => x.Send(hpChangedCommand));
        }

        public void BroadcastShield() {

            Shield = Shield; // update shield / this is useful dont fuck with this line amk / glaub du hast vergessen wozu diese line
            Controller.Send(PacketBuilder.ShieldChangeCommand(Controller));

            ICommand shieldChangedCommand = PacketBuilder.TargetHealthBaseChangedCommand(Controller);
            Controller.EntitiesLocked(x => x.Send(shieldChangedCommand));
        }

        private void BroadcastSpeed() {
            Controller.Send(PacketBuilder.SpeedChangeCommand(Controller));
        }
        #endregion

    }
}
