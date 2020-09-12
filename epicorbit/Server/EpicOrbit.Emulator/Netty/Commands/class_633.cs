using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_633 : ICommand {

        public short ID { get; set; } = 18708;
        public int maxValue = 0;
        public int currentValue = 0;
        public bool active = false;

        public class_633(int param1 = 0, int param2 = 0, bool param3 = false) {
            this.maxValue = param1;
            this.currentValue = param2;
            this.active = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.maxValue = param1.ReadInt();
            this.maxValue = param1.Shift(this.maxValue, 31);
            param1.ReadShort();
            this.currentValue = param1.ReadInt();
            this.currentValue = param1.Shift(this.currentValue, 3);
            this.active = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.maxValue, 1));
            param1.WriteShort(17706);
            param1.WriteInt(param1.Shift(this.currentValue, 29));
            param1.WriteBoolean(this.active);
            param1.WriteShort(13250);
        }
    }
}
