using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using EpicOrbit.Emulator.Netty.Commands;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class ZoneAssembly : AssemblyBase {

        #region {[ PROPERTIES ]}
        public bool IsInDMZ => _beaconCommand.protectionZoneActive;
        public bool CanEquip { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private BeaconCommand _beaconCommand;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ZoneAssembly(EntityControllerBase controller) : base(controller) {
            _beaconCommand = new BeaconCommand(0, 0, 0, 0, false, false, false, "equipment_extra_repbot_rep-4", false);
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void ShowRepairRobot() {
            if (!_beaconCommand.repairBotActive) {
                _beaconCommand.repairBotActive = true;
                Refresh();
            }
        }

        public void HideRepairBot() {
            if (_beaconCommand.repairBotActive) {
                _beaconCommand.repairBotActive = false;
                Refresh();
            }
        }

        public void ShowDMZ() {
            if (!_beaconCommand.protectionZoneActive) {
                _beaconCommand.protectionZoneActive = true;
                Refresh();
            }
        }

        public void HideDMZ() {
            if (_beaconCommand.protectionZoneActive) {
                _beaconCommand.protectionZoneActive = false;
                Refresh();
            }
        }

        public void ShowRadiationWarning() {
            if (!_beaconCommand.radiationWarning) {
                _beaconCommand.radiationWarning = true;
                Refresh();
            }
        }

        public void HideRadiationWarning() {
            if (_beaconCommand.radiationWarning) {
                _beaconCommand.radiationWarning = false;
                Refresh();
            }
        }

        public void ChangeEquip(bool state) {
            CanEquip = state;
        }
        #endregion

        public BeaconCommand ZoneCommand() {
            return _beaconCommand;
        }

        public override void Refresh() {
            Controller.Send(_beaconCommand);
        }
    }
}
