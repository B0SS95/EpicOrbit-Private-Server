using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMBoxCollectionCancelledCommand : ICommand {

        public const short BOX_ALREADY_COLLECTED = 1;
        public const short BOX_TOO_BIG = 0;
        public short ID { get; set; } = 18433;
        public LogMessengerPriorityModule priorityMode;
        public short reason = 0;

        public LMBoxCollectionCancelledCommand(LogMessengerPriorityModule param1 = null, short param2 = 0) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.reason = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            this.reason = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.priorityMode.Write(param1);
            param1.WriteShort(this.reason);
            param1.WriteShort(9003);
        }
    }
}
