using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {
    [AutoDiscover("10.0.6435")]
    public class UserKeyBindingsUpdateHandler : ICommandHandler<UserKeyBindingsUpdate> {
        public void Execute(IClient initiator, UserKeyBindingsUpdate command) {
            initiator.Controller.ClientConfiguration.KeyBindings = command.var_3733;
        }
    }
}
