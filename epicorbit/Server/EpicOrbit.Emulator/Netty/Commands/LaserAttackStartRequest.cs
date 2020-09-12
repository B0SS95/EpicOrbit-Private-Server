using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LaserAttackStartRequest : ICommand {

        public short ID { get; set; } = 27971;
        public int targetPositionY = 0;
        public int targetId = 0;
        public int targetPositionX = 0;

        public LaserAttackStartRequest(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.targetId = param1;
            this.targetPositionX = param2;
            this.targetPositionY = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.targetPositionY = param1.ReadInt();
            this.targetPositionY = param1.Shift(this.targetPositionY, 22);
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 30);
            this.targetPositionX = param1.ReadInt();
            this.targetPositionX = param1.Shift(this.targetPositionX, 31);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.targetPositionY, 10));
            param1.WriteInt(param1.Shift(this.targetId, 2));
            param1.WriteInt(param1.Shift(this.targetPositionX, 1));
            param1.WriteShort(22634);
        }
    }
}
