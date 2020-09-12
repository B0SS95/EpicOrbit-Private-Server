using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class AudioSettingsRequestHandler : ICommandHandler<AudioSettingsRequest> {
        public void Execute(IClient initiator, AudioSettingsRequest command) {
            initiator.Controller.ClientConfiguration.UserSettings.audioSettingsModule = new AudioSettingsModule(
                false, command.sound, command.music, command.var_957, command.playCombatMusic
            );
        }
    }
}
