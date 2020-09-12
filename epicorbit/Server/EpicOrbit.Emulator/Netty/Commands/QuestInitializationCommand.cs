using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestInitializationCommand : ICommand {

        public short ID { get; set; } = 31332;
        public QuestDefinitionModule quest;

        public QuestInitializationCommand(QuestDefinitionModule param1 = null) {
            if (param1 == null) {
                this.quest = new QuestDefinitionModule();
            } else {
                this.quest = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.quest = lookup.Lookup(param1) as QuestDefinitionModule;
            this.quest.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.quest.Write(param1);
        }
    }
}
