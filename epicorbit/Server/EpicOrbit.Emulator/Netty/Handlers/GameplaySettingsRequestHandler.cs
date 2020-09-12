using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class GameplaySettingsRequestHandler : ICommandHandler<GameplaySettingsRequest> {
        public void Execute(IClient initiator, GameplaySettingsRequest command) {
            initiator.Controller.ClientConfiguration.UserSettings.gameplaySettingsModule = new GameplaySettingsModule(
                false, command.autoBoost, command.autoRefinement, command.quickslotStopAttack,
                command.doubleclickAttack, command.autoChangeAmmo, command.autoStart,
                command.autoBuyGreenBootyKeys, command.showBattlerayNotifications, command.var_5281
                );
        }
    }
}
