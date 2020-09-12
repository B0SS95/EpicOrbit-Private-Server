using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerEffectsAssembly : EffectsAssembly {

        #region {[ PROPERTIES ]}
        public override bool Cloaked {
            get => PlayerController.Account.CurrentHangar.IsCloaked;
            set => PlayerController.Account.CurrentHangar.IsCloaked = value;
        }

        public PlayerController PlayerController { get; protected set; }
        #endregion

        #region {[ FIELDS ]}
        private DateTime _infectionUntil {
            get => PlayerController.Account.Cooldown.InfectionUntil;
            set => PlayerController.Account.Cooldown.InfectionUntil = value;
        }
        private bool _infected;
        private long _lastAntiInfection;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public PlayerEffectsAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
        }
        #endregion

        #region {[ INFECTION ]}
        public virtual void Infect(long duration) {
            if (!_infected) {

                if (_infectionUntil < DateTime.Now + TimeSpan.FromMilliseconds(duration)) {
                    _infectionUntil = DateTime.Now + TimeSpan.FromMilliseconds(duration);
                }
                _infected = true;

                Controller.BoosterAssembly.Multiply(BoosterType.DAMAGE, 1.1);
                Controller.BoosterAssembly.Multiply(BoosterType.HITPOINTS, 0.85);
                Controller.BoosterAssembly.Multiply(BoosterType.SPEED, 0.9);

                ICommand infectionCommand = PacketBuilder.VisualModifier(PlayerController, 56, true, ((int)Math.Abs(_infectionUntil.FromNow().TotalSeconds)).ToString());
                Controller.Send(infectionCommand);
                Controller.EntitesInRange(x => x.Send(infectionCommand));
            }
        }

        public virtual void CureInfection(bool force = false) {
            if (_infected && (_infectionUntil <= DateTime.Now || force)) {
                _infected = false;
                _infectionUntil = DateTime.Now;

                Controller.BoosterAssembly.Divide(BoosterType.DAMAGE, 1.1);
                Controller.BoosterAssembly.Divide(BoosterType.HITPOINTS, 0.85);
                Controller.BoosterAssembly.Divide(BoosterType.SPEED, 0.9);

                ICommand removeInfectionCommand = PacketBuilder.VisualModifier(PlayerController, 56, false);
                Controller.Send(removeInfectionCommand);
                Controller.EntitesInRange(x => x.Send(removeInfectionCommand));
            }
        }

        public virtual void AnitInfectionCpu() {
            const long Timeout = 5000;

            if (_infected && Controller.CurrentClock.ElapsedMilliseconds - _lastAntiInfection > Timeout) {
                if (!PlayerController.Account.Vault.Extras.TryGetValue(Extra.ANTI_Z1.ID, out int currentCount)
                        || currentCount <= 0) {
                    return;
                }
                PlayerController.Account.Vault.Extras[Extra.ANTI_Z1.ID] = --currentCount;

                CureInfection(true);
                _lastAntiInfection = Controller.CurrentClock.ElapsedMilliseconds;

                Controller.Send(
                    PacketBuilder.Slotbar.CountableItemStatus(Extra.ANTI_Z1.Name, Extra.ANTI_Z1.TTIP, currentCount, 0, false),
                    PacketBuilder.Slotbar.ItemCooldownCommand(Extra.ANTI_Z1.Name, Timeout, currentCount > 0)
                );
            }
        }
        #endregion 

        #region {[ TIMING ]}
        protected override void CheckDuration() {
            base.CheckDuration();

            if (_infected && _infectionUntil <= DateTime.Now) {
                CureInfection();
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public override IEnumerable<ICommand> EffectsCommand() {
            foreach (ICommand effect in base.EffectsCommand()) {
                yield return effect;
            }

            if (_infected) {
                yield return PacketBuilder.VisualModifier(PlayerController, 56, true, ((int)Math.Abs(_infectionUntil.FromNow().TotalSeconds)).ToString());
            }
        }
        #endregion

    }
}
