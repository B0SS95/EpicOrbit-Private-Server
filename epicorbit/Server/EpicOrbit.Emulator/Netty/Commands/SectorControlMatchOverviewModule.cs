using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlMatchOverviewModule : ICommand {

        public const short RUNNING = 1;
        public const short MATCH_MAKING = 0;
        public short ID { get; set; } = 22376;
        public int maxPlayerPerFaction = 0;
        public short matchState = 0;
        public int remainingTickets = 0;
        public int matchId = 0;
        public int playersInQueue = 0;
        public int playersOfOwnFaction = 0;
        public int totalTickets = 0;

        public SectorControlMatchOverviewModule(int param1 = 0, short param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0) {
            this.matchId = param1;
            this.matchState = param2;
            this.playersOfOwnFaction = param3;
            this.maxPlayerPerFaction = param4;
            this.remainingTickets = param5;
            this.totalTickets = param6;
            this.playersInQueue = param7;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.maxPlayerPerFaction = param1.ReadInt();
            this.maxPlayerPerFaction = param1.Shift(this.maxPlayerPerFaction, 16);
            param1.ReadShort();
            this.matchState = param1.ReadShort();
            this.remainingTickets = param1.ReadInt();
            this.remainingTickets = param1.Shift(this.remainingTickets, 10);
            this.matchId = param1.ReadInt();
            this.matchId = param1.Shift(this.matchId, 18);
            this.playersInQueue = param1.ReadInt();
            this.playersInQueue = param1.Shift(this.playersInQueue, 4);
            this.playersOfOwnFaction = param1.ReadInt();
            this.playersOfOwnFaction = param1.Shift(this.playersOfOwnFaction, 5);
            this.totalTickets = param1.ReadInt();
            this.totalTickets = param1.Shift(this.totalTickets, 25);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.maxPlayerPerFaction, 16));
            param1.WriteShort(25501);
            param1.WriteShort(this.matchState);
            param1.WriteInt(param1.Shift(this.remainingTickets, 22));
            param1.WriteInt(param1.Shift(this.matchId, 14));
            param1.WriteInt(param1.Shift(this.playersInQueue, 28));
            param1.WriteInt(param1.Shift(this.playersOfOwnFaction, 27));
            param1.WriteInt(param1.Shift(this.totalTickets, 7));
            param1.WriteShort(2674);
        }
    }
}
