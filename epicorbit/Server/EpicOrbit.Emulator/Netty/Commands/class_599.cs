using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_599 : ICommand {

        public short ID { get; set; } = 12429;
        public int var_2243 = 0;
        public int var_3072 = 0;
        public int var_688 = 0;

        public class_599(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_2243 = param1;
            this.var_3072 = param2;
            this.var_688 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2243 = param1.ReadInt();
            this.var_2243 = param1.Shift(this.var_2243, 3);
            this.var_3072 = param1.ReadInt();
            this.var_3072 = param1.Shift(this.var_3072, 4);
            this.var_688 = param1.ReadInt();
            this.var_688 = param1.Shift(this.var_688, 16);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2243, 29));
            param1.WriteInt(param1.Shift(this.var_3072, 28));
            param1.WriteInt(param1.Shift(this.var_688, 16));
        }
    }
}
