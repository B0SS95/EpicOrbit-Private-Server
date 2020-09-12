using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlMatchDetailCommand : ICommand {

        public short ID { get; set; } = 13907;
        public List<SectorControlPlayerCountModule> playerCounts;
        public List<SectorControlTicketCountCommand> ticketCounts;
        public int matchId = 0;
        public int runningTimeInSecs = 0;

        public SectorControlMatchDetailCommand(int param1 = 0, List<SectorControlTicketCountCommand> param2 = null, List<SectorControlPlayerCountModule> param3 = null, int param4 = 0) {
            this.matchId = param1;
            if (param2 == null) {
                this.ticketCounts = new List<SectorControlTicketCountCommand>();
            } else {
                this.ticketCounts = param2;
            }
            if (param3 == null) {
                this.playerCounts = new List<SectorControlPlayerCountModule>();
            } else {
                this.playerCounts = param3;
            }
            this.runningTimeInSecs = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playerCounts.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as SectorControlPlayerCountModule;
                tmp_0.Read(param1, lookup);
                this.playerCounts.Add(tmp_0);
            }
            this.ticketCounts.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as SectorControlTicketCountCommand;
                tmp_0.Read(param1, lookup);
                this.ticketCounts.Add(tmp_0);
            }
            param1.ReadShort();
            this.matchId = param1.ReadInt();
            this.matchId = param1.Shift(this.matchId, 30);
            param1.ReadShort();
            this.runningTimeInSecs = param1.ReadInt();
            this.runningTimeInSecs = param1.Shift(this.runningTimeInSecs, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.playerCounts.Count);
            foreach (var tmp_0 in this.playerCounts) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.ticketCounts.Count);
            foreach (var tmp_0 in this.ticketCounts) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(10628);
            param1.WriteInt(param1.Shift(this.matchId, 2));
            param1.WriteShort(15485);
            param1.WriteInt(param1.Shift(this.runningTimeInSecs, 18));
        }
    }
}
