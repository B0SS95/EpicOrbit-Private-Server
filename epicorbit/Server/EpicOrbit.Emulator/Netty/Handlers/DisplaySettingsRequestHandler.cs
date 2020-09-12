using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class DisplaySettingsRequestHandler : ICommandHandler<DisplaySettingsRequest> {
        public void Execute(IClient initiator, DisplaySettingsRequest command) {

            bool updateMenues = command.displayChat != initiator.Controller.ClientConfiguration.UserSettings.displaySettingsModule.displayChat;

            initiator.Controller.ClientConfiguration.UserSettings.displaySettingsModule = new DisplaySettingsCommand(
                false, command.displayPlayerName, command.displayResources, command.displayBoxes,
                command.displayHitpointBubbles, command.displayChat, command.displayDrones, command.displayCargoboxes,
                command.displayPenaltyCargoboxes, command.showNotOwnedItems, command.var_3069, command.var_3236,
                command.displayNotifications, command.preloadUserShips, command.name_161, command.var_1406, command.var_1948,
                command.var_1379, command.var_55, command.displaySetting3DqualityAntialias, command.name_42,
                command.displaySetting3DqualityEffects, command.displaySetting3DqualityLights,
                command.displaySetting3DqualityTextures, command.name_13, command.displaySetting3DsizeTextures,
                command.displaySetting3DtextureFiltering, command.proActionBarEnabled, command.proActionBarKeyboardInputEnabled,
                command.proActionBarAutohideEnabled, command.var_3558, command.var_2596
            );

            if (updateMenues) {
                initiator.Controller.Send(PacketBuilder.UIMenuBarsCommand(initiator.Controller));
            }
        }

    }
}
