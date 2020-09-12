using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using static EpicOrbit.Emulator.Game.Controllers.GroupController;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class RejectGroupInvitationRequestHandler : ICommandHandler<RejectGroupInvitationRequest> {
        public void Execute(IClient initiator, RejectGroupInvitationRequest command) {
            if (GroupController.GetForPeer(command.playerId, initiator.Controller.ID, out Invitation invitation)) {
                GroupController.RemoveForPeer(command.playerId, initiator.Controller.ID);
                invitation.Initiator.Send(PacketBuilder.Group.InvitationRejected(invitation));
            }
        }
    }
}
