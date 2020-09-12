using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {
    [AutoDiscover("10.0.6435")]
    public class LaserAttackStopRequestHandler : ICommandHandler<LaserAttackStopRequest> {
        public void Execute(IClient initiator, LaserAttackStopRequest command) {

            if (initiator.Controller.AttackAssembly.AttackRunning) {
                initiator.Controller.AttackAssembly.Stop();

                ICommand positionFixCommand = new AbortDirectionLockCommand(initiator.Controller.ID);
                initiator.Controller.EntitesInRange(x => x.Send(positionFixCommand));
            }

        }
    }
}
