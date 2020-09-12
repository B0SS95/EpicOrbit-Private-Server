using EpicOrbit.Server.Data.Models.Enumerables;
using EpicOrbit.Server.Data.Models.Items;
using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class EffectsAssembly : AssemblyBase { // displays extras like slow, ice, infection, invincible ....

        #region {[ PROPERTIES ]}
        public bool HasProtection => _invincible; // respawn protection or generic npc protection
        public virtual bool Cloaked { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private TickInterval _checkTick;
        private long _slowRocketUntil, _slowMineUntil, _iceRocketUntil, _invincibleUntil, _pldUntil, _lastCloak;
        private bool _slowedByRocket, _slowedByMine, _slowEffectActive, _iced, _invincible, _pld;
        private int _singularityCounter, _weakenShieldsCounter;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public EffectsAssembly(EntityControllerBase controller) : base(controller) {
            _checkTick = new TickInterval(CheckDuration, 100, 0);
            controller.OnTick += _checkTick.Tick;
        }
        #endregion

        #region {[ TIMING ]}
        protected virtual void CheckDuration() {
            if (_slowedByRocket && _slowRocketUntil < Controller.CurrentClock.ElapsedMilliseconds) {
                CureSlowRocket();
            }

            if (_slowedByMine && _slowMineUntil < Controller.CurrentClock.ElapsedMilliseconds) {
                CureSlowMine();
            }

            if (_iced && _iceRocketUntil < Controller.CurrentClock.ElapsedMilliseconds) {
                CureIceRocket();
            }

            if (_invincible && _invincibleUntil < Controller.CurrentClock.ElapsedMilliseconds) {
                MakeVincible();
            }

            if (_pld && _pldUntil < Controller.CurrentClock.ElapsedMilliseconds) {
                CurePLD();
            }
        }
        #endregion

        #region {[ SLOW ]}
        public virtual void SlowRocket(long duration) {
            if (_slowRocketUntil < Controller.CurrentClock.ElapsedMilliseconds + duration) {
                _slowRocketUntil = Controller.CurrentClock.ElapsedMilliseconds + duration;
            }

            if (!_slowedByRocket) {
                _slowedByRocket = true;

                Controller.BoosterAssembly.Multiply(BoosterType.SPEED, 0.7);
                SendSlowEffect();
            }
        }

        public virtual void CureSlowRocket(bool force = false) {
            if (_slowedByRocket && (_slowRocketUntil <= Controller.CurrentClock.ElapsedMilliseconds || force)) {
                _slowedByRocket = false;
                _slowRocketUntil = Controller.CurrentClock.ElapsedMilliseconds;

                Controller.BoosterAssembly.Divide(BoosterType.SPEED, 0.7);
                RemoveSlowEffect();
            }
        }

        public virtual void SlowMine(long duration) {
            if (_slowMineUntil < Controller.CurrentClock.ElapsedMilliseconds + duration) {
                _slowMineUntil = Controller.CurrentClock.ElapsedMilliseconds + duration;
            }

            if (!_slowedByMine) {
                _slowedByMine = true;

                Controller.BoosterAssembly.Multiply(BoosterType.SPEED, 0.5);
                SendSlowEffect();
            }
        }

        public virtual void CureSlowMine(bool force = false) {
            if (_slowedByMine && (_slowMineUntil <= Controller.CurrentClock.ElapsedMilliseconds || force)) {
                _slowedByMine = false;
                _slowMineUntil = Controller.CurrentClock.ElapsedMilliseconds;

                Controller.BoosterAssembly.Divide(BoosterType.SPEED, 0.5);
                RemoveSlowEffect();
            }
        }

        public void SendSlowEffect() {
            if (_slowEffectActive) {
                return;
            }

            _slowEffectActive = true;

            ICommand slowCommand = PacketBuilder.SendEffect(Controller, "SABOTEUR_DEBUFF");
            Controller.Send(slowCommand);
            Controller.EntitesInRange(x => x.Send(slowCommand));
        }

        public void RemoveSlowEffect() {
            if (!_slowEffectActive || _slowedByMine || _slowedByRocket) {
                return;
            }

            _slowEffectActive = false;

            ICommand slowCommand = PacketBuilder.RemoveEffect(Controller, "SABOTEUR_DEBUFF");
            Controller.Send(slowCommand);
            Controller.EntitesInRange(x => x.Send(slowCommand));
        }
        #endregion

        #region {[ ICE ]}
        public virtual void IceRocket(long duration) {
            if (_iceRocketUntil < Controller.CurrentClock.ElapsedMilliseconds + duration) {
                _iceRocketUntil = Controller.CurrentClock.ElapsedMilliseconds + duration;
            }

            if (!_iced) {
                _iced = true;

                Controller.BoosterAssembly.Multiply(BoosterType.SPEED, 0.00000000001);

                ICommand slowCommand = PacketBuilder.SendEffect(Controller, "ICY_CUBE");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }

        public virtual void CureIceRocket(bool force = false) {
            if (_iced && (_iceRocketUntil <= Controller.CurrentClock.ElapsedMilliseconds || force)) {
                _iced = false;
                _iceRocketUntil = Controller.CurrentClock.ElapsedMilliseconds;

                Controller.BoosterAssembly.Divide(BoosterType.SPEED, 0.00000000001);

                ICommand slowCommand = PacketBuilder.RemoveEffect(Controller, "ICY_CUBE");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }
        #endregion

        #region {[ INVICIBLE ]}
        public virtual void MakeInvincible(long duration) {
            if (_invincibleUntil < Controller.CurrentClock.ElapsedMilliseconds + duration) {
                _invincibleUntil = Controller.CurrentClock.ElapsedMilliseconds + duration;
            }

            if (!_invincible) {
                _invincible = true;

                ICommand slowCommand = PacketBuilder.SendEffect(Controller, "INVINCIBILITY");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }

        public virtual void MakeVincible() {
            if (_invincible) {
                _invincibleUntil = Controller.CurrentClock.ElapsedMilliseconds;
                _invincible = false;

                ICommand slowCommand = PacketBuilder.RemoveEffect(Controller, "INVINCIBILITY");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }
        #endregion

        #region {[ CLOAK ]}
        public virtual void Cloak() {
            const long Timeout = 3000;
            if (!Cloaked && Controller.CurrentClock.ElapsedMilliseconds - _lastCloak > Timeout) {

                if (Controller is PlayerController playerController) { // wird auch weniger

                    if (!playerController.Account.Vault.Extras.TryGetValue(Extra.CL04K.ID, out int currentCount)
                        || currentCount <= 0) {
                        return;
                    }
                    playerController.Account.Vault.Extras[Extra.CL04K.ID] = --currentCount;

                    Controller.Send(
                        PacketBuilder.Slotbar.CountableItemStatus(Extra.CL04K.Name, Extra.CL04K.TTIP, currentCount, 0, false),
                        PacketBuilder.Slotbar.ItemCooldownCommand(Extra.CL04K.Name, Timeout, currentCount > 0)
                    );
                }

                Cloaked = true;
                _lastCloak = Controller.CurrentClock.ElapsedMilliseconds;

                ICommand cloakCommand = PacketBuilder.CloakCommand(Controller);
                Controller.Send(cloakCommand);
                Controller.EntitesInRange(x => x.Send(cloakCommand));
            }
        }

        public virtual void UnCloak() {
            if (Cloaked) {
                Cloaked = false;

                IEnumerable<ICommand> cloakCommand = EffectsCommand();
                Controller.Send(cloakCommand);
                Controller.EntitesInRange(x => x.Send(cloakCommand));
            }
        }
        #endregion

        #region {[ PLD-8 ]}
        public virtual void PLDRocket(long duration) {
            if (_pldUntil < Controller.CurrentClock.ElapsedMilliseconds + duration) {
                _pldUntil = Controller.CurrentClock.ElapsedMilliseconds + duration;
            }

            if (!_pld) {
                _pld = true;

                Controller.BoosterAssembly.Multiply(BoosterType.HIT_RATE, 0.6);

                ICommand slowCommand = PacketBuilder.SendEffect2(Controller, "MAL");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }

        public virtual void CurePLD(bool force = false) {
            if (_pld && (_pldUntil <= Controller.CurrentClock.ElapsedMilliseconds || force)) {
                _pld = false;
                _pldUntil = Controller.CurrentClock.ElapsedMilliseconds;

                Controller.BoosterAssembly.Divide(BoosterType.HIT_RATE, 0.6);

                ICommand slowCommand = PacketBuilder.RemoveEffect2(Controller, "MAL");
                Controller.Send(slowCommand);
                Controller.EntitesInRange(x => x.Send(slowCommand));
            }
        }
        #endregion

        #region {[ SINGULARITY - VISUAL ]}
        public void ActivateSingularity() {
            if (_singularityCounter++ == 0) {
                ICommand effectCommand = PacketBuilder.VisualModifier(Controller, 19, true);
                Controller.Send(effectCommand);
                Controller.EntitesInRange(x => x.Send(effectCommand));
            }
        }

        public void DeactivateSingularity() {
            if (--_singularityCounter == 0) {
                ICommand effectCommand = PacketBuilder.VisualModifier(Controller, 19, false);
                Controller.Send(effectCommand);
                Controller.EntitesInRange(x => x.Send(effectCommand));
            }

            _singularityCounter = Math.Max(0, _singularityCounter);
        }
        #endregion

        #region {[ WEAKEN SHIELDS - VISUAL ]}
        public void ActivateWeakenShields() {
            if (_weakenShieldsCounter++ == 0) {
                ICommand effectCommand = PacketBuilder.VisualModifier(Controller, 17, true);
                Controller.Send(effectCommand);
                Controller.EntitesInRange(x => x.Send(effectCommand));
            }
        }

        public void DeactivateWeakenShields() {
            if (--_weakenShieldsCounter == 0) {
                ICommand effectCommand = PacketBuilder.VisualModifier(Controller, 17, false);
                Controller.Send(effectCommand);
                Controller.EntitesInRange(x => x.Send(effectCommand));
            }

            _weakenShieldsCounter = Math.Max(0, _weakenShieldsCounter);
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public virtual IEnumerable<ICommand> EffectsCommand() {
            if (Cloaked) {
                yield return PacketBuilder.CloakCommand(Controller);
            } else {
                yield return PacketBuilder.UnCloakCommand(Controller);
            }

            if (_slowEffectActive) {
                yield return PacketBuilder.SendEffect(Controller, "SABOTEUR_DEBUFF");
            }

            if (_iced) {
                yield return PacketBuilder.SendEffect(Controller, "ICY_CUBE");
            }

            if (_pld) {
                yield return PacketBuilder.SendEffect2(Controller, "MAL");
            }

            if (_invincible) {
                yield return PacketBuilder.SendEffect(Controller, "INVINCIBILITY");
            } else {
                yield return PacketBuilder.RemoveEffect(Controller, "INVINCIBILITY");
            }

            if (_singularityCounter > 0) {
                yield return PacketBuilder.VisualModifier(Controller, 19, true);
            }

            if (_weakenShieldsCounter > 0) {
                yield return PacketBuilder.VisualModifier(Controller, 17, true);
            }
        }

        public override void Refresh() {
            Controller.Send(EffectsCommand());
        }
        #endregion

    }
}
