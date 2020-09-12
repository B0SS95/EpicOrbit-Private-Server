using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class ExtraSelectionHandler : ItemSelectionHandlerBase<Extra> {

        #region {[ INSTANCE ]}
        public static ExtraSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new ExtraSelectionHandler();
                }
                return _instance;
            }
        }
        private static ExtraSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out Extra extra)) {
                if (extra.ID == Extra.CL04K.ID) { // cloak
                    playerController.EffectsAssembly.Cloak();
                } else if (extra.ID == Extra.ANTI_Z1.ID) {
                    playerController.PlayerEffectsAssembly.AnitInfectionCpu();
                } else if (extra.ID == Extra.AROL_X.ID) {
                    playerController.Account.CurrentHangar.Selection.AutoRocketCpu =
                        !playerController.Account.CurrentHangar.Selection.AutoRocketCpu;

                    playerController.Send(PacketBuilder.Slotbar.ExtraItemStatus(extra.Name, extra.TTIP,
                        playerController.Account.CurrentHangar.Selection.AutoRocketCpu));
                } else if (extra.ID == Extra.RL_LB_X.ID) {
                    playerController.Account.CurrentHangar.Selection.AutoRocketLauncherCpu =
                        !playerController.Account.CurrentHangar.Selection.AutoRocketLauncherCpu;

                    playerController.Send(PacketBuilder.Slotbar.ExtraItemStatus(extra.Name, extra.TTIP,
                        playerController.Account.CurrentHangar.Selection.AutoRocketLauncherCpu));
                }
            }
        }
        #endregion

    }

}
