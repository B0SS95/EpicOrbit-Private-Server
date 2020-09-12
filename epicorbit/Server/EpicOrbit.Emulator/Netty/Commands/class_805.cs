using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_805 : ICommand {

        public short ID { get; set; } = 31384;
        public int assetId = 0;
        public int timer = 0;
        public int var_983 = 0;

        public class_805(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_983 = param1;
            this.assetId = param2;
            this.timer = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.assetId = param1.ReadInt();
            this.assetId = param1.Shift(this.assetId, 26);
            param1.ReadShort();
            this.timer = param1.ReadInt();
            this.timer = param1.Shift(this.timer, 16);
            this.var_983 = param1.ReadInt();
            this.var_983 = param1.Shift(this.var_983, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.assetId, 6));
            param1.WriteShort(12719);
            param1.WriteInt(param1.Shift(this.timer, 16));
            param1.WriteInt(param1.Shift(this.var_983, 2));
        }
    }
}
