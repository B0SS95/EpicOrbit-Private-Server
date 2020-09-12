using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlMatchOverviewCommand : ICommand {

        public short ID { get; set; } = 9895;
        public int minPlayersPerTeam = 0;
        public int minLevel = 0;
        public List<SectorControlMatchOverviewModule> var_2983;
        public int maxLevel = 0;
        public int queuedForMatch = 0;

        public SectorControlMatchOverviewCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, List<SectorControlMatchOverviewModule> param5 = null) {
            this.minLevel = param1;
            this.maxLevel = param2;
            this.minPlayersPerTeam = param3;
            this.queuedForMatch = param4;
            if (param5 == null) {
                this.var_2983 = new List<SectorControlMatchOverviewModule>();
            } else {
                this.var_2983 = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.minPlayersPerTeam = param1.ReadInt();
            this.minPlayersPerTeam = param1.Shift(this.minPlayersPerTeam, 17);
            this.minLevel = param1.ReadInt();
            this.minLevel = param1.Shift(this.minLevel, 24);
            this.var_2983.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as SectorControlMatchOverviewModule;
                tmp_0.Read(param1, lookup);
                this.var_2983.Add(tmp_0);
            }
            this.maxLevel = param1.ReadInt();
            this.maxLevel = param1.Shift(this.maxLevel, 25);
            this.queuedForMatch = param1.ReadInt();
            this.queuedForMatch = param1.Shift(this.queuedForMatch, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(27321);
            param1.WriteInt(param1.Shift(this.minPlayersPerTeam, 15));
            param1.WriteInt(param1.Shift(this.minLevel, 8));
            param1.WriteInt(this.var_2983.Count);
            foreach (var tmp_0 in this.var_2983) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.maxLevel, 7));
            param1.WriteInt(param1.Shift(this.queuedForMatch, 18));
        }
    }
}
