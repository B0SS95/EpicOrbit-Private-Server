using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HuntEventRankingPointsUpdateCommand : ICommand {

        public const short CLAN = 1;
        public const short PLAYER = 0;
        public short ID { get; set; } = 8463;
        public short updateMode = 0;
        public int score = 0;
        public int eventId = 0;

        public HuntEventRankingPointsUpdateCommand(short param1 = 0, int param2 = 0, int param3 = 0) {
            this.updateMode = param1;
            this.eventId = param2;
            this.score = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.updateMode = param1.ReadShort();
            this.score = param1.ReadInt();
            this.score = param1.Shift(this.score, 4);
            this.eventId = param1.ReadInt();
            this.eventId = param1.Shift(this.eventId, 23);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.updateMode);
            param1.WriteInt(param1.Shift(this.score, 28));
            param1.WriteInt(param1.Shift(this.eventId, 9));
        }
    }
}
