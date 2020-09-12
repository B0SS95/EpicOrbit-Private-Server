using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1007 : ICommand {

        public const short const_353 = 1;
        public const short const_654 = 3;
        public const short const_2745 = 0;
        public const short const_3198 = 5;
        public const short const_2179 = 2;
        public const short const_1313 = 4;
        public short ID { get; set; } = 7714;
        public short var_3096 = 0;

        public class_1007(short param1 = 0) {
            this.var_3096 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3096 = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_3096);
            param1.WriteShort(27317);
            param1.WriteShort(27107);
        }
    }
}
