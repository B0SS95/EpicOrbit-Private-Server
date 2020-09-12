using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Game.Implementations;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class AttackTraceAssembly : AssemblyBase {

        public class AttackTraceEntry { // wir brauchen die Referenz

            public AttackTraceEntry(long time, int shieldDamage, int hitpointsDamage) {
                InitialAttackTime = time;
                LastAttackTime = time;
                ShieldDamageDealt = shieldDamage;
                HitpointsDamageDealt = hitpointsDamage;
            }

            public long InitialAttackTime { get; set; }
            public long LastAttackTime { get; set; }
            public int ShieldDamageDealt { get; set; }
            public int HitpointsDamageDealt { get; set; }

        }

        #region {[ PROPERTIES ]}
        public Dictionary<int, AttackTraceEntry> Trace => _trace;
        public long LastAttackTime { get; set; }
        public int CurrentMainAttacker { get; set; } = -1;
        #endregion

        #region {[ FIELDS ]}
        private Dictionary<int, AttackTraceEntry> _trace;
        private TickInterval _validationTimer;
        private object _lock;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public AttackTraceAssembly(EntityControllerBase controller) : base(controller) {
            _trace = new Dictionary<int, AttackTraceEntry>();
            _lock = new object();

            _validationTimer = new TickInterval(CheckChanges, 500);
            controller.OnTick += _validationTimer.Tick;
        }
        #endregion

        #region {[ HANDLER ]}
        public void ShieldRepair(int change) {
            lock (_lock) {
                double changeFactor = (Controller.HangarAssembly.Shield - change * 1.0) / Controller.HangarAssembly.Shield;

                List<int> toRemove = new List<int>();
                foreach (var pair in _trace) {
                    pair.Value.ShieldDamageDealt = (int)(pair.Value.ShieldDamageDealt * changeFactor);
                    if (pair.Value.ShieldDamageDealt == 0 && pair.Value.HitpointsDamageDealt == 0) {
                        toRemove.Add(pair.Key);
                    }
                }

                foreach (int id in toRemove) {
                    _trace.Remove(id);
                }
            }
        }

        public void HitpointsRepair(int change) {
            lock (_lock) {
                double changeFactor = (Controller.HangarAssembly.Hitpoints - change * 1.0) / Controller.HangarAssembly.Hitpoints;

                List<int> toRemove = new List<int>();
                foreach (var pair in _trace) {
                    pair.Value.HitpointsDamageDealt = (int)(pair.Value.HitpointsDamageDealt * changeFactor);
                    if (pair.Value.ShieldDamageDealt == 0 && pair.Value.HitpointsDamageDealt == 0) {
                        toRemove.Add(pair.Key);
                    }
                }

                foreach (int id in toRemove) {
                    _trace.Remove(id);
                }
            }
        }

        private void CheckChanges() {
            const long LockTimeout = 7000;

            lock (_lock) {
                long currentTime = Controller.CurrentClock.ElapsedMilliseconds; // set time
                if (currentTime - LastAttackTime > LockTimeout  // kein angriff seit 7 sekunden
                    || !_trace.TryGetValue(CurrentMainAttacker, out AttackTraceEntry traceEntry) // angreifer nicht verfügbar
                    || currentTime - traceEntry.LastAttackTime > LockTimeout) { // angreifer hat seit 7 sekunden keinen schaden gemacht

                    bool attackerFound = false;
                    foreach (var pair in _trace.OrderBy(x => x.Value.InitialAttackTime)) {
                        if (currentTime - pair.Value.LastAttackTime > LockTimeout) {
                            continue;
                        }

                        attackerFound = true;
                        ChangeMainAttacker(pair.Key);
                        break;
                    }

                    if (!attackerFound) {
                        ChangeMainAttacker(-1);
                    }
                }
            }
        }

        private void ChangeMainAttacker(int newAttacker) {
            if (CurrentMainAttacker == newAttacker) {
                return;
            }

            CurrentMainAttacker = newAttacker;

            ICommand scopeUnsetCommand = PacketBuilder.MainAttackerUnsetCommand(Controller);
            if (newAttacker == -1) {
                Controller.EntitiesLocked(x => x.Send(scopeUnsetCommand));
            } else {
                ICommand scopeSetCommand = PacketBuilder.MainAttackerChangedCommand(Controller, newAttacker);
                Controller.EntitiesLocked(x => {
                    if (x.ID == newAttacker) {
                        x.Send(scopeUnsetCommand);
                    } else {
                        x.Send(scopeSetCommand);
                    }
                });
            }
        }

        private void HandleDependencies(EntityControllerBase attacker, bool affectEffects = true) {
            if (affectEffects) {
                if (attacker is PlayerController enemyPlayerController && enemyPlayerController.IsInLogoutProcess) {
                    enemyPlayerController.CancelLogout();
                }

                if (attacker.EffectsAssembly.HasProtection && attacker is PlayerController) { // loose protection
                    attacker.EffectsAssembly.MakeVincible();
                }

                if (attacker.EffectsAssembly.Cloaked) {
                    attacker.EffectsAssembly.UnCloak();
                }
            }

            if (Controller is PlayerController ownPlayerController && ownPlayerController.IsInLogoutProcess) {
                ownPlayerController.CancelLogout();
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void LogAttack(EntityControllerBase attacker, int shieldDamage, int hitpointsDamage, bool affectEffects = true) {
            HandleDependencies(attacker, affectEffects);

            long currentTime = Controller.CurrentClock.ElapsedMilliseconds; // set time
            LastAttackTime = currentTime;

            if (_trace.TryGetValue(attacker.ID, out AttackTraceEntry traceEntry)) {
                traceEntry.LastAttackTime = currentTime;
                traceEntry.ShieldDamageDealt += shieldDamage;
                traceEntry.HitpointsDamageDealt += hitpointsDamage;
            } else {
                _trace.Add(attacker.ID, new AttackTraceEntry(currentTime, shieldDamage, hitpointsDamage));
            }

            if (CurrentMainAttacker == -1) {
                ChangeMainAttacker(attacker.ID);
            }
        }

        public void Reset() {
            lock (_lock) {
                _trace.Clear();
                CurrentMainAttacker = -1;
                LastAttackTime = 0;
            }
        }
        #endregion

        public override void Refresh() { }
    }
}
