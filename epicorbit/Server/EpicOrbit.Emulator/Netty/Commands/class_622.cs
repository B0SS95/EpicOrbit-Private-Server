using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_622 : ICommand {

        public short ID { get; set; } = 30032;
        public int x = 0;
        public int color = 0;
        public int y = 0;
        public int var_3274 = 0;

        public class_622(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.x = param1;
            this.y = param2;
            this.color = param3;
            this.var_3274 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 17);
            this.color = param1.ReadInt();
            this.color = param1.Shift(this.color, 7);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 19);
            this.var_3274 = param1.ReadInt();
            this.var_3274 = param1.Shift(this.var_3274, 13);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.x, 15));
            param1.WriteInt(param1.Shift(this.color, 25));
            param1.WriteInt(param1.Shift(this.y, 13));
            param1.WriteInt(param1.Shift(this.var_3274, 19));
            param1.WriteShort(-13095);
        }
    }
}
