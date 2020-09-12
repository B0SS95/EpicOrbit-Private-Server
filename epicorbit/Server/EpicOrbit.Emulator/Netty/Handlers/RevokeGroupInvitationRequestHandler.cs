using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using static EpicOrbit.Emulator.Game.Controllers.GroupController;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class RevokeGroupInvitationRequestHandler : ICommandHandler<RevokeGroupInvitationRequest> {
        public void Execute(IClient initiator, RevokeGroupInvitationRequest command) {
            if (GroupController.GetForPeer(initiator.Controller.ID, command.playerId, out Invitation invitation)) {
                GroupController.RemoveForPeer(initiator.Controller.ID, command.playerId);
                invitation.Target.Send(PacketBuilder.Group.InvitationRevoked(invitation));
            }
        }
    }
}
