using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_582 : ICommand {

        public short ID { get; set; } = 28426;
        public int range = 0;
        public int uid = 0;
        public int color = 0;
        public int var_3274 = 0;

        public class_582(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.uid = param1;
            this.range = param2;
            this.color = param3;
            this.var_3274 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.range = param1.ReadInt();
            this.range = param1.Shift(this.range, 14);
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 17);
            this.color = param1.ReadInt();
            this.color = param1.Shift(this.color, 13);
            this.var_3274 = param1.ReadInt();
            this.var_3274 = param1.Shift(this.var_3274, 25);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.range, 18));
            param1.WriteInt(param1.Shift(this.uid, 15));
            param1.WriteInt(param1.Shift(this.color, 19));
            param1.WriteInt(param1.Shift(this.var_3274, 7));
            param1.WriteShort(27495);
        }
    }
}
