using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_763 : ICommand {

        public short ID { get; set; } = 2005;
        public int var_298 = 0;
        public int var_5030 = 0;

        public class_763(int param1 = 0, int param2 = 0) {
            this.var_298 = param1;
            this.var_5030 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_298 = param1.ReadInt();
            this.var_298 = param1.Shift(this.var_298, 11);
            this.var_5030 = param1.ReadInt();
            this.var_5030 = param1.Shift(this.var_5030, 18);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_298, 21));
            param1.WriteInt(param1.Shift(this.var_5030, 14));
            param1.WriteShort(-3417);
        }
    }
}
