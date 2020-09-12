using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Shared.Items;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class TechFactorySelectionHandler : ItemSelectionHandlerBase<TechFactory> {

        #region {[ INSTANCE ]}
        public static TechFactorySelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new TechFactorySelectionHandler();
                }
                return _instance;
            }
        }
        private static TechFactorySelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out TechFactory tech)) {
                if (tech.ID == TechFactory.ENERGY_LEECH.ID) {
                    playerController.PlayerTechAssembly.StartEnergyTransfer();
                } else if (tech.ID == TechFactory.BATTLE_REPAIR_BOT.ID) {
                    playerController.PlayerTechAssembly.StartBattleRepairBot();
                } else if (tech.ID == TechFactory.SHIELD_BACKUP.ID) {
                    playerController.PlayerTechAssembly.StartShieldBackup();
                } else if (tech.ID == TechFactory.PRECISION_TARGETER.ID) {
                    playerController.PlayerTechAssembly.StartPrecisionTargeter();
                } else if (tech.ID == TechFactory.CHAIN_IMPULSE.ID) {
                    playerController.PlayerTechAssembly.StartChainImpulse();
                }
            }
        }
        #endregion

    }

}
