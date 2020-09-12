using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipSelectionRequest : ICommand {

        public short ID { get; set; } = 23442;
        public int targetPositionX = 0;
        public int sourcePositionX = 0;
        public int targetId = 0;
        public int targetPositionY = 0;
        public int sourcePositionY = 0;

        public ShipSelectionRequest(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.targetId = param1;
            this.targetPositionX = param2;
            this.targetPositionY = param3;
            this.sourcePositionX = param4;
            this.sourcePositionY = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.targetPositionX = param1.ReadInt();
            this.targetPositionX = param1.Shift(this.targetPositionX, 5);
            this.sourcePositionX = param1.ReadInt();
            this.sourcePositionX = param1.Shift(this.sourcePositionX, 31);
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 10);
            this.targetPositionY = param1.ReadInt();
            this.targetPositionY = param1.Shift(this.targetPositionY, 20);
            this.sourcePositionY = param1.ReadInt();
            this.sourcePositionY = param1.Shift(this.sourcePositionY, 13);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5594);
            param1.WriteInt(param1.Shift(this.targetPositionX, 27));
            param1.WriteInt(param1.Shift(this.sourcePositionX, 1));
            param1.WriteInt(param1.Shift(this.targetId, 22));
            param1.WriteInt(param1.Shift(this.targetPositionY, 12));
            param1.WriteInt(param1.Shift(this.sourcePositionY, 19));
        }
    }
}
