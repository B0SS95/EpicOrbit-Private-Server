using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestConditionUpdateCommand : ICommand {

        public short ID { get; set; } = 26842;
        public QuestConditionStateModule state;
        public int questConditionId = 0;

        public QuestConditionUpdateCommand(int param1 = 0, QuestConditionStateModule param2 = null) {
            this.questConditionId = param1;
            if (param2 == null) {
                this.state = new QuestConditionStateModule();
            } else {
                this.state = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.state = lookup.Lookup(param1) as QuestConditionStateModule;
            this.state.Read(param1, lookup);
            this.questConditionId = param1.ReadInt();
            this.questConditionId = param1.Shift(this.questConditionId, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-9769);
            this.state.Write(param1);
            param1.WriteInt(param1.Shift(this.questConditionId, 10));
        }
    }
}
