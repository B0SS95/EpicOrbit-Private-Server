using EpicOrbit.Emulator.Game;
using EpicOrbit.Emulator.Game.Controllers;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Linq;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class GroupInvitationRequestHandler : ICommandHandler<GroupInvitationRequest> {

        public void Execute(IClient initiator, GroupInvitationRequest command) {

            PlayerController target = GameManager.Players.Select(x => x.Value).FirstOrDefault(x => x.Username == command.name);
            if (target == null || target.ID == initiator.Controller.ID) {
                initiator.Send(PacketBuilder.Group.PlayerDoesNotExist());
            } else if (target.PlayerGroupAssembly.Group != null) {
                initiator.Send(PacketBuilder.Group.PlayerAlreadyInGroup());
            } else if (!target.PlayerGroupAssembly.AcceptInvitations) {
                initiator.Send(PacketBuilder.Group.InviationBlocked());
            } else {
                GroupController.EnqueueInvitation(initiator.Controller, target);
            }

        }

    }
}
