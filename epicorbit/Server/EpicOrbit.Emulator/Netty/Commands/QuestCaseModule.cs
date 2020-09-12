using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestCaseModule : ICommand {

        public short ID { get; set; } = 27434;
        public bool mandatory = false;
        public int mandatoryCount = 0;
        public int id = 0;
        public List<QuestElementModule> modifier;
        public bool ordered = false;
        public bool active = false;

        public QuestCaseModule(int param1 = 0, bool param2 = false, bool param3 = false, bool param4 = false, int param5 = 0, List<QuestElementModule> param6 = null) {
            this.id = param1;
            this.active = param2;
            this.mandatory = param3;
            this.ordered = param4;
            this.mandatoryCount = param5;
            if (param6 == null) {
                this.modifier = new List<QuestElementModule>();
            } else {
                this.modifier = param6;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mandatory = param1.ReadBoolean();
            this.mandatoryCount = param1.ReadInt();
            this.mandatoryCount = param1.Shift(this.mandatoryCount, 27);
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 9);
            this.modifier.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestElementModule;
                tmp_0.Read(param1, lookup);
                this.modifier.Add(tmp_0);
            }
            this.ordered = param1.ReadBoolean();
            param1.ReadShort();
            this.active = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.mandatory);
            param1.WriteInt(param1.Shift(this.mandatoryCount, 5));
            param1.WriteInt(param1.Shift(this.id, 23));
            param1.WriteInt(this.modifier.Count);
            foreach (var tmp_0 in this.modifier) {
                tmp_0.Write(param1);
            }
            param1.WriteBoolean(this.ordered);
            param1.WriteShort(16952);
            param1.WriteBoolean(this.active);
            param1.WriteShort(19938);
        }
    }
}
