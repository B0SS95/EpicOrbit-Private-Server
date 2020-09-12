using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestDefinitionModule : ICommand {

        public short ID { get; set; } = 12182;
        public List<QuestTypeModule> types;
        public string var_3286 = "";
        public int id = 0;
        public string name_16 = "";
        public List<QuestIconModule> icons;
        public List<class_891> rewards;
        public QuestCaseModule rootCase;

        public QuestDefinitionModule(int param1 = 0, List<QuestTypeModule> param2 = null, QuestCaseModule param3 = null, List<class_891> param4 = null, List<QuestIconModule> param5 = null, string param6 = "", string param7 = "") {
            this.id = param1;
            if (param2 == null) {
                this.types = new List<QuestTypeModule>();
            } else {
                this.types = param2;
            }
            if (param3 == null) {
                this.rootCase = new QuestCaseModule();
            } else {
                this.rootCase = param3;
            }
            if (param4 == null) {
                this.rewards = new List<class_891>();
            } else {
                this.rewards = param4;
            }
            if (param5 == null) {
                this.icons = new List<QuestIconModule>();
            } else {
                this.icons = param5;
            }
            this.var_3286 = param6;
            this.name_16 = param7;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.types.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestTypeModule;
                tmp_0.Read(param1, lookup);
                this.types.Add(tmp_0);
            }
            this.var_3286 = param1.ReadUTF();
            param1.ReadShort();
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 13);
            this.name_16 = param1.ReadUTF();
            this.icons.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestIconModule;
                tmp_0.Read(param1, lookup);
                this.icons.Add(tmp_0);
            }
            this.rewards.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_891;
                tmp_0.Read(param1, lookup);
                this.rewards.Add(tmp_0);
            }
            this.rootCase = lookup.Lookup(param1) as QuestCaseModule;
            this.rootCase.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.types.Count);
            foreach (var tmp_0 in this.types) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.var_3286);
            param1.WriteShort(-9142);
            param1.WriteInt(param1.Shift(this.id, 19));
            param1.WriteUTF(this.name_16);
            param1.WriteInt(this.icons.Count);
            foreach (var tmp_0 in this.icons) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.rewards.Count);
            foreach (var tmp_0 in this.rewards) {
                tmp_0.Write(param1);
            }
            this.rootCase.Write(param1);
        }
    }
}
