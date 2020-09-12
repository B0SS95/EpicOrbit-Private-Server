using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class MoveRequestHandler : ICommandHandler<MoveRequest> {

        public void Execute(IClient initiator, MoveRequest command) {
            if (initiator.Controller.IsInLogoutProcess) {
                initiator.Controller.CancelLogout();
            }

            initiator.Controller.MovementAssembly.Move(
                new Position(command.positionX, command.positionY),
                new Position(command.targetX, command.targetY)
            );
        }

    }
}
