using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {
    [AutoDiscover("10.0.6435")]
    public class SaveBarsSettingsRequestHandler : ICommandHandler<SaveBarsSettingsRequest> {
        public void Execute(IClient initiator, SaveBarsSettingsRequest command) {
            initiator.Controller.ClientConfiguration.UserSettings.windowSettingsModule.minimapScaleFactor = command.minimapScaleFactor;
            initiator.Controller.ClientConfiguration.UserSettings.windowSettingsModule.barState = command.barState;
            initiator.Controller.ClientConfiguration.SlotbarPositions = command;
        }
    }
}
