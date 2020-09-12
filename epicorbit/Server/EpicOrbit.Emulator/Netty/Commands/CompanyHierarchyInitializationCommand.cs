using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CompanyHierarchyInitializationCommand : ICommand {

        public short ID { get; set; } = 2173;
        public List<CompanyHierarchyRankingModule> vruRanking;
        public FactionModule ownFaction;
        public List<CompanyHierarchyRankingModule> mmoRanking;
        public List<CompanyHierarchyRankingModule> eicRanking;
        public CompanyHierarchyRankingModule ownRanking;

        public CompanyHierarchyInitializationCommand(List<CompanyHierarchyRankingModule> param1 = null, List<CompanyHierarchyRankingModule> param2 = null, List<CompanyHierarchyRankingModule> param3 = null, CompanyHierarchyRankingModule param4 = null, FactionModule param5 = null) {
            if (param1 == null) {
                this.mmoRanking = new List<CompanyHierarchyRankingModule>();
            } else {
                this.mmoRanking = param1;
            }
            if (param2 == null) {
                this.eicRanking = new List<CompanyHierarchyRankingModule>();
            } else {
                this.eicRanking = param2;
            }
            if (param3 == null) {
                this.vruRanking = new List<CompanyHierarchyRankingModule>();
            } else {
                this.vruRanking = param3;
            }
            if (param4 == null) {
                this.ownRanking = new CompanyHierarchyRankingModule();
            } else {
                this.ownRanking = param4;
            }
            if (param5 == null) {
                this.ownFaction = new FactionModule();
            } else {
                this.ownFaction = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.vruRanking.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as CompanyHierarchyRankingModule;
                tmp_0.Read(param1, lookup);
                this.vruRanking.Add(tmp_0);
            }
            this.ownFaction = lookup.Lookup(param1) as FactionModule;
            this.ownFaction.Read(param1, lookup);
            param1.ReadShort();
            this.mmoRanking.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as CompanyHierarchyRankingModule;
                tmp_0.Read(param1, lookup);
                this.mmoRanking.Add(tmp_0);
            }
            this.eicRanking.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as CompanyHierarchyRankingModule;
                tmp_0.Read(param1, lookup);
                this.eicRanking.Add(tmp_0);
            }
            param1.ReadShort();
            this.ownRanking = lookup.Lookup(param1) as CompanyHierarchyRankingModule;
            this.ownRanking.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.vruRanking.Count);
            foreach (var tmp_0 in this.vruRanking) {
                tmp_0.Write(param1);
            }
            this.ownFaction.Write(param1);
            param1.WriteShort(-20645);
            param1.WriteInt(this.mmoRanking.Count);
            foreach (var tmp_0 in this.mmoRanking) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.eicRanking.Count);
            foreach (var tmp_0 in this.eicRanking) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(712);
            this.ownRanking.Write(param1);
        }
    }
}
