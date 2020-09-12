using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class RocketAmunitionSelectionHandler : ItemSelectionHandlerBase<RocketAmmunition> {

        #region {[ INSTANCE ]}
        public static RocketAmunitionSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new RocketAmunitionSelectionHandler();
                }
                return _instance;
            }
        }
        private static RocketAmunitionSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out RocketAmmunition rocket)) {
                RocketAmmunition oldRocket = playerController.Account.CurrentHangar.Selection.Rocket.FromRocketAmmunitions();

                playerController.Account.Vault.RocketAmmunitions.TryGetValue(oldRocket.ID, out int oldRocketAmmunitionCount);
                playerController.Account.Vault.RocketAmmunitions.TryGetValue(rocket.ID, out int rocketAmmunitionCount);

                playerController.Send(
                    PacketBuilder.Slotbar.RocketItemStatus(oldRocket.Name, oldRocketAmmunitionCount, false),
                    PacketBuilder.Slotbar.RocketItemStatus(rocket.Name, rocketAmmunitionCount, true)
                );

                playerController.Account.CurrentHangar.Selection.Rocket = rocket.ID;
                if (initAttack) {
                    int dummyLapNumber = 0;
                    (playerController.AttackAssembly as PlayerAttackAssembly).RocketAttack(ref dummyLapNumber, rocket, true);
                }
            }
        }
        #endregion

    }

}
