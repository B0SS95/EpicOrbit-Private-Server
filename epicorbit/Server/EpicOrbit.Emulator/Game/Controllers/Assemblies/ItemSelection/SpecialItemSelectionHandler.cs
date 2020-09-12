using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class SpecialItemSelectionHandler : ItemSelectionHandlerBase<SpecialItem> {

        #region {[ INSTANCE ]}
        public static SpecialItemSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new SpecialItemSelectionHandler();
                }
                return _instance;
            }
        }
        private static SpecialItemSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out SpecialItem ammuninition)) {
                if (ammuninition.ID == SpecialItem.SMB_01.ID) {
                    playerController.SpecialItemsAssembly.TriggerSMB();
                } else if (ammuninition.ID == SpecialItem.EMP_01.ID) {
                    playerController.SpecialItemsAssembly.TriggerEMP();
                } else if (ammuninition.ID == SpecialItem.ISH_01.ID) {
                    playerController.SpecialItemsAssembly.TriggerISH();
                }
            }
        }
        #endregion

    }

}
