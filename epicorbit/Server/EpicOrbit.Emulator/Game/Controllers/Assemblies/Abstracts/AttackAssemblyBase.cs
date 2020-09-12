using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Implementations;
using System;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts {
    public abstract class AttackAssemblyBase : AssemblyBase {

        #region {[ STATIC ]}
        protected static Random random = new Random();
        #endregion

        #region {[ PROPERTIES ]}
        public long LastAttack { get; protected set; }
        public bool AttackRunning => _attackRunning;
        public bool TargetOutOfRange => _targetOutOfRange;
        #endregion

        #region {[ FIELDS ]}
        protected bool _attackRunning, _targetOutOfRange;
        protected TickInterval _attackTimer;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public AttackAssemblyBase(EntityControllerBase controller) : base(controller) {
            _attackTimer = new TickInterval(AttackTimer, 1000, 1000); // set initial value to 1000, so the first tick is an attack
            Controller.OnTick += Tick;
        }
        #endregion

        #region {[ HELPER ]}
        protected abstract void AttackTimer();
        #endregion

        #region {[ FUNCTIONS ]}
        public abstract void Start();
        public abstract void Stop();
        #endregion

        #region {[ TIMING ]}
        protected abstract void Tick(double changeSinceLastTime);
        #endregion

        public override void Refresh() { }

    }
}
