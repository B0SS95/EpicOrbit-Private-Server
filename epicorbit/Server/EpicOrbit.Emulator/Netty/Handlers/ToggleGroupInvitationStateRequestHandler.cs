using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class ToggleGroupInvitationStateRequestHandler : ICommandHandler<ToggleGroupInvitationStateRequest> {
        public void Execute(IClient initiator, ToggleGroupInvitationStateRequest command) {

            initiator.Controller.PlayerGroupAssembly.AcceptInvitations = !initiator.Controller.PlayerGroupAssembly.AcceptInvitations;
            initiator.Controller.Send(PacketBuilder.Group.InvitiationState(initiator.Controller));

        }
    }

}
