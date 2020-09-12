using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMItemCollectedCommand : ICommand {

        public short ID { get; set; } = 15695;
        public LogMessengerPriorityModule priorityMode;
        public string lootId = "";

        public LMItemCollectedCommand(LogMessengerPriorityModule param1 = null, string param2 = "") {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.lootId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            this.lootId = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.priorityMode.Write(param1);
            param1.WriteUTF(this.lootId);
            param1.WriteShort(-7946);
        }
    }
}
