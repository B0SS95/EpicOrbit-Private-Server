using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {
    [AutoDiscover("10.0.6435")]
    public class LegacyModuleHandler : ICommandHandler<LegacyModule> {
        public void Execute(IClient initiator, LegacyModule command) {
            if (command.message.StartsWith("S|CFG")) {
                initiator.Controller.PlayerHangarAssembly.ChangeConfiguration();
            }
        }
    }
}
