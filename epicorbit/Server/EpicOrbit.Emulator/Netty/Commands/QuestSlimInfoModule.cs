using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestSlimInfoModule : ICommand {

        public short ID { get; set; } = 6174;
        public int rootCaseId = 0;
        public string var_3286 = ""; // title, description?
        public QuestAcceptabilityStatusModule acceptabilityStatus;
        public string name_16 = ""; // title, description?
        public List<QuestTypeModule> types;
        public List<QuestRequirementModule> missingAcceptRequirements;
        public int priority = 0;
        public QuestIconModule icon;
        public int questId = 0;
        public int minLevel = 0;

        public QuestSlimInfoModule(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, List<QuestTypeModule> param5 = null, QuestIconModule param6 = null, QuestAcceptabilityStatusModule param7 = null, List<QuestRequirementModule> param8 = null, string param9 = "", string param10 = "") {
            this.questId = param1;
            this.rootCaseId = param2;
            this.minLevel = param3;
            this.priority = param4;
            if (param5 == null) {
                this.types = new List<QuestTypeModule>();
            } else {
                this.types = param5;
            }
            if (param6 == null) {
                this.icon = new QuestIconModule();
            } else {
                this.icon = param6;
            }
            if (param7 == null) {
                this.acceptabilityStatus = new QuestAcceptabilityStatusModule();
            } else {
                this.acceptabilityStatus = param7;
            }
            if (param8 == null) {
                this.missingAcceptRequirements = new List<QuestRequirementModule>();
            } else {
                this.missingAcceptRequirements = param8;
            }
            this.var_3286 = param9;
            this.name_16 = param10;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.rootCaseId = param1.ReadInt();
            this.rootCaseId = param1.Shift(this.rootCaseId, 4);
            this.var_3286 = param1.ReadUTF();
            this.acceptabilityStatus = lookup.Lookup(param1) as QuestAcceptabilityStatusModule;
            this.acceptabilityStatus.Read(param1, lookup);
            param1.ReadShort();
            this.name_16 = param1.ReadUTF();
            this.types.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestTypeModule;
                tmp_0.Read(param1, lookup);
                this.types.Add(tmp_0);
            }
            this.missingAcceptRequirements.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestRequirementModule;
                tmp_0.Read(param1, lookup);
                this.missingAcceptRequirements.Add(tmp_0);
            }
            this.priority = param1.ReadInt();
            this.priority = param1.Shift(this.priority, 30);
            this.icon = lookup.Lookup(param1) as QuestIconModule;
            this.icon.Read(param1, lookup);
            this.questId = param1.ReadInt();
            this.questId = param1.Shift(this.questId, 18);
            this.minLevel = param1.ReadInt();
            this.minLevel = param1.Shift(this.minLevel, 4);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.rootCaseId, 28));
            param1.WriteUTF(this.var_3286);
            this.acceptabilityStatus.Write(param1);
            param1.WriteShort(2297);
            param1.WriteUTF(this.name_16);
            param1.WriteInt(this.types.Count);
            foreach (var tmp_0 in this.types) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.missingAcceptRequirements.Count);
            foreach (var tmp_0 in this.missingAcceptRequirements) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.priority, 2));
            this.icon.Write(param1);
            param1.WriteInt(param1.Shift(this.questId, 14));
            param1.WriteInt(param1.Shift(this.minLevel, 28));
        }
    }
}
