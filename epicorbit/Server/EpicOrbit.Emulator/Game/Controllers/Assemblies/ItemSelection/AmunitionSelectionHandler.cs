using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts;
using EpicOrbit.Emulator.Netty;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Shared.Items;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection {

    public class AmunitionSelectionHandler : ItemSelectionHandlerBase<Ammuninition> {

        #region {[ INSTANCE ]}
        public static AmunitionSelectionHandler Instance {
            get {
                if (_instance == null) {
                    _instance = new AmunitionSelectionHandler();
                }
                return _instance;
            }
        }
        private static AmunitionSelectionHandler _instance;
        #endregion

        #region {[ FUNCTIONS ]}
        public override void Handle(PlayerController playerController, string itemId, bool initAttack) {
            if (Lookup.TryGetValue(itemId, out Ammuninition ammuninition)) {
                Ammuninition oldAmmunition = playerController.Account.CurrentHangar.Selection.Laser.FromAmmunitions();

                playerController.Account.Vault.Ammunitions.TryGetValue(oldAmmunition.ID, out int oldAmmunitionCount);
                playerController.Account.Vault.Ammunitions.TryGetValue(ammuninition.ID, out int ammunitionCount);

                playerController.Send(
                    PacketBuilder.Slotbar.LaserItemStatus(oldAmmunition.Name, oldAmmunitionCount, false),
                    PacketBuilder.Slotbar.LaserItemStatus(ammuninition.Name, ammunitionCount, true)
                );

                playerController.Account.CurrentHangar.Selection.Laser = ammuninition.ID;

                // rsb, bei rsb gilt nur der eigene cooldown
                if (playerController.AttackAssembly.AttackRunning && ammuninition.ID == Ammuninition.RSB_75.ID && ammuninition.ID != oldAmmunition.ID) {
                    int dummyLapNumber = 0;
                    (playerController.AttackAssembly as PlayerAttackAssembly).LaserAttack(ref dummyLapNumber, Ammuninition.RSB_75);
                }

                if (initAttack) {
                    if (playerController.AttackAssembly.AttackRunning && oldAmmunition.ID == ammuninition.ID) {
                        playerController.AttackAssembly.Stop();

                        ICommand positionFixCommand = new AbortDirectionLockCommand(playerController.ID);
                        playerController.EntitesInRange(x => x.Send(positionFixCommand));
                    } else {
                        playerController.AttackAssembly.Start();
                    }
                }
            }
        }
        #endregion

    }

}
