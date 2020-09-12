using EpicOrbit.Emulator.Game.Controllers.Assemblies;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Game.Implementations;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Shared.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EpicOrbit.Emulator.Game.Controllers.Abstracts {

    public abstract class EntityControllerBase : IDisposable {

        #region {[ TIMING ]}
        public OffsetStopwatch CurrentClock { get; set; }
        public event Action<double> OnTick;
        protected bool _ready;
        private Timer _timer;
        private double _last;
        #endregion

        #region {[ PROPERTIES ]}
        public int ID { get; protected set; }
        public string Username { get; protected set; }
        public Faction Faction { get; protected set; }

        public SpacemapController Spacemap { get; set; }
        public EntityControllerBase Locked { get; set; }

        public MovementAssembly MovementAssembly { get; protected set; }
        public HangarAssembly HangarAssembly { get; protected set; }
        public BoosterAssembly BoosterAssembly { get; protected set; }
        public AttackAssemblyBase AttackAssembly { get; protected set; }
        public EffectsAssembly EffectsAssembly { get; protected set; }
        public AttackTraceAssembly AttackTraceAssembly { get; protected set; }
        public ZoneAssembly ZoneAssembly { get; protected set; }
        #endregion

        #region {[ FIELDS ]}
        protected IGameLogger _logger;
        private bool _disposed;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public EntityControllerBase(int id, string username, Faction faction) {
            _logger = GameContext.Logger ?? throw new ArgumentNullException("invalid logger!");

            ID = id;
            Username = username;
            Faction = faction;

            CurrentClock = new OffsetStopwatch(TimeSpan.FromSeconds(10));
        }
        #endregion

        #region {[ COMMANDS ]}
        public abstract void Send(ICommand command);
        public abstract void Send(params ICommand[] commands);
        public abstract void Send(IEnumerable<ICommand> commands);

        public void EntitesInRange(Action<EntityControllerBase> handler) {
            if (Spacemap != null) {
                foreach (var entity in Spacemap.InRange(this)) {
                    handler(entity);
                }
            }
        }

        public void EntitiesInRangeSafe(Action<EntityControllerBase> handler) {
            if (Spacemap != null) {
                foreach (var entity in Spacemap.InRange(this).ToList()) {
                    handler(entity);
                }
            }
        }

        public void EntitiesLocked(Action<EntityControllerBase> handler) {
            if (Spacemap != null) {
                foreach (var entity in Spacemap.Locked(this)) {
                    handler(entity);
                }
            }
        }

        public void EntitiesLockedSafe(Action<EntityControllerBase> handler) {
            if (Spacemap != null) {
                foreach (var entity in Spacemap.Locked(this).ToList()) {
                    handler(entity);
                }
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public virtual async void Lock(EntityControllerBase entity) {

            if (Locked == null || entity == null || Locked.ID != entity.ID) {
                bool wasAttacking = AttackAssembly.AttackRunning;
                if (wasAttacking) {
                    AttackAssembly.Stop();
                    ICommand positionFixCommand = new AbortDirectionLockCommand(ID);
                    EntitesInRange(x => x.Send(positionFixCommand));
                }
            }

            if (entity == null) {
                Locked = null;
                Send(PacketBuilder.DeselectionCommand());
            } else if (entity.ID != ID) {
                if (entity is PlayerController playerController && playerController.SpecialItemsAssembly.IsNotTargetable) {
                    return;
                }

                Locked = entity;
                SendLockVisual(entity);
            }
        }

        protected void SendLockVisual(EntityControllerBase entity) {
            if (entity.AttackTraceAssembly.CurrentMainAttacker == -1) {
                Send(PacketBuilder.SelectCommand(entity));
            } else {
                Send(
                    PacketBuilder.SelectCommand(entity),
                    PacketBuilder.MainAttackerChangedCommand(entity, entity.AttackTraceAssembly.CurrentMainAttacker)
                );
            }
        }

        public void InitializeTimer() {
            int frequency = 10 * (GetType() == typeof(PlayerController) ? 1 : 3);
            _timer = new Timer(x => {
                Tick();
            }, null, TimeSpan.FromMilliseconds(frequency), TimeSpan.FromMilliseconds(frequency));
        }

        public async void Tick() {
            if (_ready) {
                double _current = CurrentClock.TotalElapsedMilliseconds;

                if (_current - _last < 0) {
                    return;
                }

                OnTick?.Invoke(_current - _last);
                _last = _current;
            }
        }

        public void TimerStart() {
            CurrentClock.Start();
            _last = CurrentClock.TotalElapsedMilliseconds;
            _ready = true;
        }

        public void TimerStop() {
            CurrentClock.Stop();
            _ready = false;
        }

        public abstract void Refresh();
        public abstract void EntityAddedToMap(EntityControllerBase entity);
        public abstract void EntityRemovedFromMap(int id);
        public abstract void Die();
        #endregion

        #region {[ DISPOSE & DESTRUCTOR ]}
        public virtual void Dispose() {
            if (_disposed || !(_disposed = true)) {
                return;
            }

            TimerStop();
            _timer.Dispose();
        }

        ~EntityControllerBase() {
            Dispose();
        }
        #endregion

    }

}
