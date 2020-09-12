using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMDiscountInfoCommand : ICommand {

        public const short LOOT = 0;
        public const short REWARD = 1;
        public short ID { get; set; } = 12041;
        public LogMessengerPriorityModule priorityMode;
        public short discountType = 0;
        public double percentage = 0;

        public LMDiscountInfoCommand(LogMessengerPriorityModule param1 = null, short param2 = 0, double param3 = 0) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.discountType = param2;
            this.percentage = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            this.discountType = param1.ReadShort();
            this.percentage = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.priorityMode.Write(param1);
            param1.WriteShort(this.discountType);
            param1.WriteDouble(this.percentage);
        }
    }
}
