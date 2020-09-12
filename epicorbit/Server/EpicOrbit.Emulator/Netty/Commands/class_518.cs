using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_518 : ICommand {

        public const short const_2389 = 3;
        public const short const_1923 = 1;
        public const short const_1041 = 0;
        public const short const_300 = 4;
        public const short RED_BLINK = 5;
        public const short ARROW = 2;
        public const short ACTIVE = 6;
        public short ID { get; set; } = 6295;
        public short type = 0;

        public class_518(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
        }
    }
}
