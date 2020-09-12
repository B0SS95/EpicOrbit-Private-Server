using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class StatusModule : ICommand {

        public const short CRAFTING = 0;
        public const short ACTIVE = 2;
        public const short COOLING_DOWN = 3;
        public const short READY = 1;
        public const short INACTIVE = 4;
        public short ID { get; set; } = 22545;

        public StatusModule() {
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(11321);
        }
    }
}
