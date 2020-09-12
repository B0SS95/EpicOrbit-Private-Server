using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_616 : ICommand {

        public const short const_2561 = 1;
        public const short const_3209 = 0;
        public short ID { get; set; } = 1942;
        public short var_2988 = 0;

        public class_616(short param1 = 0) {
            this.var_2988 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2988 = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_2988);
            param1.WriteShort(-2115);
            param1.WriteShort(-25111);
        }
    }
}
