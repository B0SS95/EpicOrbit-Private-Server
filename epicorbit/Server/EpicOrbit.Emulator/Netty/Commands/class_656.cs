using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_656 : ICommand {

        public const short const_3163 = 0;
        public const short const_329 = 2;
        public const short const_555 = 3;
        public const short const_1817 = 1;
        public short ID { get; set; } = 18540;
        public short type = 0;

        public class_656(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
            param1.WriteShort(8753);
        }
    }
}
