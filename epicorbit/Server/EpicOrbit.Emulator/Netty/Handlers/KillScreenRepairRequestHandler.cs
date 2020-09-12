using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Commands;
using EpicOrbit.Emulator.Netty.Implementations;
using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Handlers {

    [AutoDiscover("10.0.6435")]
    public class KillScreenRepairRequestHandler : ICommandHandler<KillScreenRepairRequest> {
        public void Execute(IClient initiator, KillScreenRepairRequest command) {

            initiator.Controller.HangarAssembly.ChangeHitpoints(1000, false, false);

            DataOutputStream outputStream = new DataOutputStream();
            command.requestModule.Write(outputStream);
            byte[] result = outputStream.GetData();

            initiator.Receive(result.SubArray(2, result.Length - 2));

        }
    }

}
