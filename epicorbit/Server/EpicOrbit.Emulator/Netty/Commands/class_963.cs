using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_963 : ICommand {

        public const short WINDOW = 2;
        public const short const_1568 = 0;
        public const short const_439 = 1;
        public short ID { get; set; } = 3706;
        public short var_5008 = 0;

        public class_963(short param1 = 0) {
            this.var_5008 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_5008 = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-16098);
            param1.WriteShort(this.var_5008);
            param1.WriteShort(22964);
        }
    }
}
