using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using System.Linq;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class ShipAbilitySelectionHandler : ItemSelectionHandlerBase<ShipAbility> {

        #region {[ INSTANCE ]}
        public static ShipAbilitySelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new ShipAbilitySelectionHandler();
                }
                return _instance;
            }
        }
        private static ShipAbilitySelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override async void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out ShipAbility ability)
                && playerController.HangarAssembly.Ship.Abilities.Any(x => x.ID == ability.ID)) {
                // man kann das system nicht umgehen und 
                // selber fähigkeits command schicken lul
                if (ability.ID == ShipAbility.AFTERBURNER.ID) {
                    playerController.PlayerAbilityAssembly.StartAfterburner();
                } else if (ability.ID == ShipAbility.FORTRESS.ID) {
                    playerController.PlayerAbilityAssembly.StartFortress();
                } else if (ability.ID == ShipAbility.PRISMATIC_SHIELD.ID) {
                    playerController.PlayerAbilityAssembly.StartPrismaticShield();
                } else if (ability.ID == ShipAbility.SINGULARITY.ID) {
                    playerController.PlayerAbilityAssembly.StartSingularity(7400, 300);
                } else if (ability.ID == ShipAbility.NANO_CLUSTER_REPAIR.ID) {
                    playerController.PlayerAbilityAssembly.StartNanoClusterRepair();
                } else if (ability.ID == ShipAbility.WEAKEN_SHIELDS.ID) {
                    playerController.PlayerAbilityAssembly.StartWeakenShields();
                }
            }
        }
        #endregion

    }

}
