using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MoveRequest : ICommand {

        public short ID { get; set; } = 2601;
        public int targetY = 0;
        public int targetX = 0;
        public int positionY = 0;
        public int positionX = 0;

        public MoveRequest(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.positionX = param1;
            this.targetY = param2;
            this.targetX = param3;
            this.positionY = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.targetY = param1.ReadInt();
            this.targetY = param1.Shift(this.targetY, 19);
            this.targetX = param1.ReadInt();
            this.targetX = param1.Shift(this.targetX, 1);
            this.positionY = param1.ReadInt();
            this.positionY = param1.Shift(this.positionY, 11);
            this.positionX = param1.ReadInt();
            this.positionX = param1.Shift(this.positionX, 20);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.targetY, 13));
            param1.WriteInt(param1.Shift(this.targetX, 31));
            param1.WriteInt(param1.Shift(this.positionY, 21));
            param1.WriteInt(param1.Shift(this.positionX, 12));
        }
    }
}
