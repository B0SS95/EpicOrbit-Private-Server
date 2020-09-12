using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMCollectAmmunitionCommand : ICommand {

        public short ID { get; set; } = 4583;
        public AmmunitionTypeModule collectedAmmunitionType;
        public int collectedAmount = 0;
        public LogMessengerPriorityModule priorityMode;

        public LMCollectAmmunitionCommand(LogMessengerPriorityModule param1 = null, AmmunitionTypeModule param2 = null, int param3 = 0) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            if (param2 == null) {
                this.collectedAmmunitionType = new AmmunitionTypeModule();
            } else {
                this.collectedAmmunitionType = param2;
            }
            this.collectedAmount = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.collectedAmmunitionType = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.collectedAmmunitionType.Read(param1, lookup);
            this.collectedAmount = param1.ReadInt();
            this.collectedAmount = param1.Shift(this.collectedAmount, 16);
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.collectedAmmunitionType.Write(param1);
            param1.WriteInt(param1.Shift(this.collectedAmount, 16));
            this.priorityMode.Write(param1);
        }
    }
}
