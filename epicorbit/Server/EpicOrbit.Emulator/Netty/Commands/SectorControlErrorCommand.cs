using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlErrorCommand : ICommand {

        public const short QUEUE_FULL = 2;
        public const short NO_TICKETS_LEFT = 1;
        public const short LEVEL_TOO_LOW = 0;
        public short ID { get; set; } = 24354;
        public short errorType = 0;

        public SectorControlErrorCommand(short param1 = 0) {
            this.errorType = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.errorType = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.errorType);
        }
    }
}
