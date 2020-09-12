using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Linq;

namespace EpicOrbit.Emulator.Netty.Handlers {
    [AutoDiscover("10.0.6435")]
    public class ShipSelectionRequestHandler : ICommandHandler<ShipSelectionRequest> {
        public void Execute(IClient initiator, ShipSelectionRequest command) {

            if (command.targetId != initiator.Controller.ID) {
                var target = initiator.Controller.Spacemap.InRange(initiator.Controller).Where(x => x.ID == command.targetId).FirstOrDefault();
                if (target != null) {
                    initiator.Controller.Lock(target);
                } else {
                    GameContext.Logger.LogCritical("Selection did not succees ???!?!??!?!?");
                }
            }

        }
    }
}
