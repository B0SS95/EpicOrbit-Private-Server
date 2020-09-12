using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class RocketAttackStartRequest : ICommand {

        public short ID { get; set; } = 25245;
        public int targetPositionY = 0;
        public int targetPositionX = 0;
        public int targetID = 0;

        public RocketAttackStartRequest(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.targetID = param1;
            this.targetPositionX = param2;
            this.targetPositionY = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.targetPositionY = param1.ReadInt();
            this.targetPositionY = param1.Shift(this.targetPositionY, 18);
            this.targetPositionX = param1.ReadInt();
            this.targetPositionX = param1.Shift(this.targetPositionX, 30);
            this.targetID = param1.ReadInt();
            this.targetID = param1.Shift(this.targetID, 23);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(30758);
            param1.WriteInt(param1.Shift(this.targetPositionY, 14));
            param1.WriteInt(param1.Shift(this.targetPositionX, 2));
            param1.WriteInt(param1.Shift(this.targetID, 9));
            param1.WriteShort(9534);
        }
    }
}
