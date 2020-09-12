using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {
    public class DroneFormationSelectionHandler : ItemSelectionHandlerBase<DroneFormation> {

        #region {[ INSTANCE ]}
        public static DroneFormationSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new DroneFormationSelectionHandler();
                }
                return _instance;
            }
        }
        private static DroneFormationSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out DroneFormation droneFormation)
                && playerController.Account.Vault.DroneFormations.Contains(droneFormation.ID)) {
                string oldItemId = playerController.DroneFormationAssembly.DroneFormation.Name;
                if (playerController.DroneFormationAssembly.ChangeFormation(droneFormation)) {

                    ICommand[] commands = new ICommand[_items.Count + 2];
                    commands[0] = PacketBuilder.Slotbar.DroneFormationItemStatus(itemId, true);
                    commands[1] = PacketBuilder.Slotbar.DroneFormationItemStatus(oldItemId, false);

                    for (int i = 0; i < Items.Count; i++) {
                        commands[i + 2] = PacketBuilder.Slotbar.ItemCooldownCommand(_items[i], playerController.DroneFormationAssembly.FormationCooldown);
                    }

                    playerController.Send(commands);

                }
            }
        }
        #endregion

    }
}
