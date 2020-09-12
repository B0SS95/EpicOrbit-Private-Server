using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackRocketCommand : ICommand {

        public short ID { get; set; } = 23690;
        public int rocketType = 0;
        public int targetId = 0;
        public bool isHeatSeeking = false;
        public int attackerId = 0;
        public bool hit = false;
        public int smokeId = 0;

        public AttackRocketCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, bool param5 = false, bool param6 = false) {
            this.attackerId = param1;
            this.targetId = param2;
            this.rocketType = param3;
            this.smokeId = param4;
            this.hit = param5;
            this.isHeatSeeking = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.rocketType = param1.ReadInt();
            this.rocketType = param1.Shift(this.rocketType, 23);
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 16);
            this.isHeatSeeking = param1.ReadBoolean();
            param1.ReadShort();
            this.attackerId = param1.ReadInt();
            this.attackerId = param1.Shift(this.attackerId, 22);
            param1.ReadShort();
            this.hit = param1.ReadBoolean();
            this.smokeId = param1.ReadInt();
            this.smokeId = param1.Shift(this.smokeId, 9);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.rocketType, 9));
            param1.WriteInt(param1.Shift(this.targetId, 16));
            param1.WriteBoolean(this.isHeatSeeking);
            param1.WriteShort(-10650);
            param1.WriteInt(param1.Shift(this.attackerId, 10));
            param1.WriteShort(28613);
            param1.WriteBoolean(this.hit);
            param1.WriteInt(param1.Shift(this.smokeId, 23));
        }
    }
}
