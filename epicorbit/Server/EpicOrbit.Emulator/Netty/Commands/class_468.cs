using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_468 : ICommand {

        public short ID { get; set; } = 16037;
        public int var_1970 = 0;
        public int var_3884 = 0;
        public int var_4995 = 0;

        public class_468(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_4995 = param1;
            this.var_3884 = param2;
            this.var_1970 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1970 = param1.ReadInt();
            this.var_1970 = param1.Shift(this.var_1970, 16);
            param1.ReadShort();
            this.var_3884 = param1.ReadInt();
            this.var_3884 = param1.Shift(this.var_3884, 6);
            this.var_4995 = param1.ReadInt();
            this.var_4995 = param1.Shift(this.var_4995, 11);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1970, 16));
            param1.WriteShort(-26347);
            param1.WriteInt(param1.Shift(this.var_3884, 26));
            param1.WriteInt(param1.Shift(this.var_4995, 21));
        }
    }
}
