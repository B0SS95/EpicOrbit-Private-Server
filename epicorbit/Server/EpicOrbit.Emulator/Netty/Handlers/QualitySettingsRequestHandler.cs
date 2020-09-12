using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class QualitySettingsRequestHandler : ICommandHandler<QualitySettingsRequest> {
        public void Execute(IClient initiator, QualitySettingsRequest command) {
            initiator.Controller.ClientConfiguration.UserSettings.qualitySettingsModule = new QualitySettingsModule(
                false, command.qualityAttack, command.qualityBackground, command.qualityPresetting,
                command.qualityCustomized, command.qualityPOIzone, command.qualityShip,
                command.qualityEngine, command.qualityExplosion, command.qualityCollectables,
                command.qualityEffect
            );
        }
    }
}
