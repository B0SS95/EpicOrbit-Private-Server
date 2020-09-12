using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class AcceptGroupInvitationRequestHandler : ICommandHandler<AcceptGroupInvitationRequest> {
        public void Execute(IClient initiator, AcceptGroupInvitationRequest command) {

        }
    }
}
