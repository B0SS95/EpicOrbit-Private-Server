using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LogMessengerPriorityModule : ICommand {

        public const short STANDARD = 0;
        public const short HIGH_PRIORITY = 1;
        public short ID { get; set; } = 6704;
        public short priorityModeValue = 0;

        public LogMessengerPriorityModule(short param1 = 0) {
            this.priorityModeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.priorityModeValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.priorityModeValue);
            param1.WriteShort(21570);
        }
    }
}
