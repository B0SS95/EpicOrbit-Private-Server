using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestRequirementModule : ICommand {

        public const short NUMBER_OF_QUESTS_ACCEPTED = 1;
        public const short LEVEL = 0;
        public const short const_3466 = 5;
        public const short DATE = 2;
        public const short EVENT = 4;
        public const short PRE_QUEST = 3;
        public short ID { get; set; } = 2039;
        public List<class_1065> matches;
        public short requirementType = 0;
        public List<class_1065> missingMatches;
        public double maxValue = 0;
        public double minValue = 0;

        public QuestRequirementModule(short param1 = 0, double param2 = 0, double param3 = 0, List<class_1065> param4 = null, List<class_1065> param5 = null) {
            this.requirementType = param1;
            this.minValue = param2;
            this.maxValue = param3;
            if (param4 == null) {
                this.matches = new List<class_1065>();
            } else {
                this.matches = param4;
            }
            if (param5 == null) {
                this.missingMatches = new List<class_1065>();
            } else {
                this.missingMatches = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.matches.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_1065;
                tmp_0.Read(param1, lookup);
                this.matches.Add(tmp_0);
            }
            this.requirementType = param1.ReadShort();
            param1.ReadShort();
            this.missingMatches.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_1065;
                tmp_0.Read(param1, lookup);
                this.missingMatches.Add(tmp_0);
            }
            this.maxValue = param1.ReadDouble();
            this.minValue = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.matches.Count);
            foreach (var tmp_0 in this.matches) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(this.requirementType);
            param1.WriteShort(14282);
            param1.WriteInt(this.missingMatches.Count);
            foreach (var tmp_0 in this.missingMatches) {
                tmp_0.Write(param1);
            }
            param1.WriteDouble(this.maxValue);
            param1.WriteDouble(this.minValue);
        }
    }
}
