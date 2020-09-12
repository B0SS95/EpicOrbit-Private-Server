using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Linq;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.ViewModels.Boost;
using EpicOrbit.Shared.Items.Extensions;
using EpicOrbit.Emulator.Game.Implementations;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class PlayerDroneFormationAssembly : AssemblyBase {

        #region {[ CONSTANTS ]}
        private readonly long FORMATION_COOLDOWN = 2000;
        #endregion

        #region {[ PROPERTIES ]}
        public long FormationCooldown => FORMATION_COOLDOWN;
        public PlayerController PlayerController { get; set; }
        public DroneFormation DroneFormation {
            get {
                if (_droneFormation == null) {
                    _droneFormation = PlayerController.Account.CurrentHangar.Selection.Formation.FromDroneFormations();
                }
                return _droneFormation;
            }
            set {
                _droneFormation = value;
                PlayerController.Account.CurrentHangar.Selection.Formation = _droneFormation.ID;
            }
        }
        #endregion

        #region {[ FIELDS ]}
        private TickInterval _shieldChangeTickTimeFunction;
        private double _shieldChangePerSecond;
        private long _lastFormationChangeTime;
        private DroneFormation _droneFormation;
        #endregion

        #region {[ CONSTRUCTOR & INITIALIZER ]}
        public PlayerDroneFormationAssembly(PlayerController controller) : base(controller) {
            PlayerController = controller;
            _shieldChangeTickTimeFunction = new TickInterval(ChangeShield, 1000);

            Initialize();
            PlayerController.OnTick += Tick;
        }

        private void Initialize() {
            AddBoosts(DroneFormation);
            _shieldChangePerSecond = GetShieldChangePerSecond();
        }
        #endregion

        #region {[ HELPER ]}
        private double GetShieldChangePerSecond() {
            switch (DroneFormation.ID) {
                case DroneFormation.DIAMOND_ID:
                    return 0.01;
                case DroneFormation.MOTH_ID:
                case DroneFormation.WHEEL_ID:
                    return -0.05;
                case DroneFormation.DOME_ID:
                    return 0.015;
            }
            return 0;
        }

        private void AddBoosts(DroneFormation formation) {
            foreach (var boost in formation.Stats) {
                Controller.BoosterAssembly.Multiply(boost.Type, boost.Amount);
            }
        }

        private void MergeBoosts(DroneFormation old, DroneFormation current) {
            foreach (var boost in old.Stats) {
                var currentBoost = current.Stats.Cast<BoostView?>().FirstOrDefault(x => x.Value.Type == boost.Type);
                if (currentBoost != null) {
                    double newBoost = Controller.BoosterAssembly.Get(boost.Type) / boost.Amount;
                    Controller.BoosterAssembly.Set(boost.Type, newBoost * currentBoost.Value.Amount);
                } else {
                    Controller.BoosterAssembly.Divide(boost.Type, boost.Amount);
                }
            }

            foreach (var boost in current.Stats) {
                if (!old.Stats.Any(x => x.Type == boost.Type)) {
                    Controller.BoosterAssembly.Multiply(boost.Type, boost.Amount);
                }
            }
        }
        #endregion

        public bool ChangeFormation(DroneFormation change) {
            long currentTime = PlayerController.CurrentClock.ElapsedMilliseconds;
            if (DroneFormation.ID == change.ID || _lastFormationChangeTime.FromNow(currentTime) < FORMATION_COOLDOWN) {
                return false;
            };

            _lastFormationChangeTime = currentTime;
            MergeBoosts(DroneFormation, change);

            DroneFormation = change;
            _shieldChangeTickTimeFunction.Reset();
            _shieldChangePerSecond = GetShieldChangePerSecond();

            ICommand formationChangedCommand = PacketBuilder.DroneFormationChangeCommand(PlayerController);
            PlayerController.Send(formationChangedCommand);
            PlayerController.EntitesInRange((x) => x.Send(formationChangedCommand));

            return true;
        }

        public override void Refresh() { }

        private void ChangeShield() {
            if ((Math.Sign(_shieldChangePerSecond) == 1 && Controller.HangarAssembly.Shield == Controller.HangarAssembly.MaxShield)
                           || (Math.Sign(_shieldChangePerSecond) == -1 && Controller.HangarAssembly.Shield == 0)) {
                return; // unnötig was zu verändern wenn maximum eh schon erreicht wurde
            }

            int change = (int)(Controller.HangarAssembly.MaxShield * _shieldChangePerSecond);
            if (DroneFormation.ID == DroneFormation.DIAMOND_ID) {
                change = Math.Min(5000, change);
            }

            Controller.HangarAssembly.ChangeShield(change, ignoreEffects: true);
        }

        private void Tick(double changeSinceLastTime) {
            if (_shieldChangePerSecond != 0) {
                _shieldChangeTickTimeFunction.Tick(changeSinceLastTime);
            }
        }

    }
}
