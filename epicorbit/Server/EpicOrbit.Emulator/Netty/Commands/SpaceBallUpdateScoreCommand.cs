using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SpaceBallUpdateScoreCommand : ICommand {

        public short ID { get; set; } = 8182;
        public int score = 0;
        public int gateId = 0;
        public int factionId = 0;

        public SpaceBallUpdateScoreCommand(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.factionId = param1;
            this.score = param2;
            this.gateId = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.score = param1.ReadInt();
            this.score = param1.Shift(this.score, 1);
            this.gateId = param1.ReadInt();
            this.gateId = param1.Shift(this.gateId, 15);
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 17);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-3364);
            param1.WriteInt(param1.Shift(this.score, 31));
            param1.WriteInt(param1.Shift(this.gateId, 17));
            param1.WriteInt(param1.Shift(this.factionId, 15));
            param1.WriteShort(19509);
        }
    }
}
