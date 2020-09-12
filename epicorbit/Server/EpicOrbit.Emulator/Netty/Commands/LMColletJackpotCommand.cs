using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMColletJackpotCommand : ICommand {

        public short ID { get; set; } = 25915;
        public float collectedAmount = 0;
        public float summedAmmount = 0;
        public LogMessengerPriorityModule priority;

        public LMColletJackpotCommand(LogMessengerPriorityModule param1 = null, float param2 = 0, float param3 = 0) {
            if (param1 == null) {
                this.priority = new LogMessengerPriorityModule();
            } else {
                this.priority = param1;
            }
            this.collectedAmount = param2;
            this.summedAmmount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.collectedAmount = param1.ReadFloat();
            param1.ReadShort();
            this.summedAmmount = param1.ReadFloat();
            this.priority = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priority.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteFloat(this.collectedAmount);
            param1.WriteShort(-15168);
            param1.WriteFloat(this.summedAmmount);
            this.priority.Write(param1);
        }
    }
}
