using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestElementModule : ICommand {

        public short ID { get; set; } = 14797;
        public QuestCaseModule questCase;
        public QuestConditionModule condition;

        public QuestElementModule(QuestCaseModule param1 = null, QuestConditionModule param2 = null) {
            if (param1 == null) {
                this.questCase = new QuestCaseModule();
            } else {
                this.questCase = param1;
            }
            if (param2 == null) {
                this.condition = new QuestConditionModule();
            } else {
                this.condition = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.questCase = lookup.Lookup(param1) as QuestCaseModule;
            this.questCase.Read(param1, lookup);
            param1.ReadShort();
            this.condition = lookup.Lookup(param1) as QuestConditionModule;
            this.condition.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.questCase.Write(param1);
            param1.WriteShort(6651);
            this.condition.Write(param1);
            param1.WriteShort(-7152);
        }
    }
}
