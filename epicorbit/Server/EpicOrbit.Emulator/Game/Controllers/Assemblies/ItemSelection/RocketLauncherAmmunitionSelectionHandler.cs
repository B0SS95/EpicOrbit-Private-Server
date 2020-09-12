using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class RocketLauncherAmunitionSelectionHandler : ItemSelectionHandlerBase<RocketLauncherAmmunition> {

        #region {[ INSTANCE ]}
        public static RocketLauncherAmunitionSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new RocketLauncherAmunitionSelectionHandler();
                }
                return _instance;
            }
        }
        private static RocketLauncherAmunitionSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out RocketLauncherAmmunition rlAmmo)) {

                if (playerController.Account.CurrentHangar.Selection.RocketLauncher != rlAmmo.ID) {
                    playerController.Account.CurrentHangar.Selection.RocketLauncher = rlAmmo.ID;
                    playerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount = 0;
                    playerController.Send(PacketBuilder.Slotbar.RocketLauncherStateCommand(playerController));
                }

                if (initAttack) {
                    (playerController.AttackAssembly as PlayerAttackAssembly).RocketLauncherAttack(playerController.Account.CurrentHangar.Selection.RocketLauncherLoadedCount, rlAmmo);
                }

            }
        }
        #endregion

    }

}
