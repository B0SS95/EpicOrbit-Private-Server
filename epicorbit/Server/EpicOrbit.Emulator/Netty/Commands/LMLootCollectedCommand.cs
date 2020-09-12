using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMLootCollectedCommand : ICommand {

        public short ID { get; set; } = 12002;
        public LogMessengerPriorityModule priorityMode;
        public int collectedAmount = 0;
        public string lootId = "";

        public LMLootCollectedCommand(LogMessengerPriorityModule param1 = null, string param2 = "", int param3 = 0) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.lootId = param2;
            this.collectedAmount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            this.collectedAmount = param1.ReadInt();
            this.collectedAmount = param1.Shift(this.collectedAmount, 21);
            this.lootId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-7267);
            this.priorityMode.Write(param1);
            param1.WriteInt(param1.Shift(this.collectedAmount, 11));
            param1.WriteUTF(this.lootId);
        }
    }
}
