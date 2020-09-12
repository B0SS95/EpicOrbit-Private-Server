using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Game.Controllers.Assemblies;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Shared.Items.Extensions;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class SelectMenuBarItemHandler : ICommandHandler<SelectMenuBarItem> {
        public void Execute(IClient initiator, SelectMenuBarItem command) {
            bool initAttack = command.doubleClick == 1 || command.var_2253 == 1;

            if (AmunitionSelectionHandler.Instance.Contains(command.ItemId)) {
                AmunitionSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (RocketAmunitionSelectionHandler.Instance.Contains(command.ItemId)) {
                RocketAmunitionSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (RocketLauncherAmunitionSelectionHandler.Instance.Contains(command.ItemId)) {
                RocketLauncherAmunitionSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (MineSelectionHandler.Instance.Contains(command.ItemId)) {
                MineSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (ExtraSelectionHandler.Instance.Contains(command.ItemId)) {
                ExtraSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (SpecialItemSelectionHandler.Instance.Contains(command.ItemId)) {
                SpecialItemSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (TechFactorySelectionHandler.Instance.Contains(command.ItemId)) {
                TechFactorySelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (ShipAbilitySelectionHandler.Instance.Contains(command.ItemId)) {
                ShipAbilitySelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (DroneFormationSelectionHandler.Instance.Contains(command.ItemId)) {
                DroneFormationSelectionHandler.Instance.Handle(initiator.Controller, command.ItemId, initAttack);
            } else if (command.ItemId == "equipment_weapon_rocketlauncher_hst") {
                if ((initiator.Controller as PlayerController).Account.CurrentHangar.Selection.RocketLauncher == 0) {
                    return;
                }

                (initiator.Controller.AttackAssembly as PlayerAttackAssembly).RocketLauncherAttack(
                    initiator.Controller.Account.CurrentHangar.Selection.RocketLauncherLoadedCount,
                    initiator.Controller.Account.CurrentHangar.Selection.RocketLauncher.FromRocketLauncherAmmunitions());
            }

        }
    }

}
