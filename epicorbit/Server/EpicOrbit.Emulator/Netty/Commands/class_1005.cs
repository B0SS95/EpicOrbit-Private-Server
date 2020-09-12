using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1005 : ICommand {

        public short ID { get; set; } = 29918;
        public int var_3494 = 0;
        public int var_1850 = 0;
        public int var_1195 = 0;

        public class_1005(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_1195 = param1;
            this.var_1850 = param2;
            this.var_3494 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3494 = param1.ReadInt();
            this.var_3494 = param1.Shift(this.var_3494, 22);
            this.var_1850 = param1.ReadInt();
            this.var_1850 = param1.Shift(this.var_1850, 4);
            param1.ReadShort();
            this.var_1195 = param1.ReadInt();
            this.var_1195 = param1.Shift(this.var_1195, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_3494, 10));
            param1.WriteInt(param1.Shift(this.var_1850, 28));
            param1.WriteShort(-25067);
            param1.WriteInt(param1.Shift(this.var_1195, 2));
        }
    }
}
