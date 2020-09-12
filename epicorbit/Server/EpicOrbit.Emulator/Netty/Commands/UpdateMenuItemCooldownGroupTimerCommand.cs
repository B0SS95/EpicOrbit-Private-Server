using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UpdateMenuItemCooldownGroupTimerCommand : ICommand {

        public short ID { get; set; } = 3491;
        public ClientUISlotBarCategoryItemTimerStateModule timerState;
        public double time = 0;
        public CooldownTypeModule cooldownType;
        public double maxTime = 0;

        public UpdateMenuItemCooldownGroupTimerCommand(CooldownTypeModule param1 = null, ClientUISlotBarCategoryItemTimerStateModule param2 = null, double param3 = 0, double param4 = 0) {
            if (param1 == null) {
                this.cooldownType = new CooldownTypeModule();
            } else {
                this.cooldownType = param1;
            }
            if (param2 == null) {
                this.timerState = new ClientUISlotBarCategoryItemTimerStateModule();
            } else {
                this.timerState = param2;
            }
            this.time = param3;
            this.maxTime = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.timerState = lookup.Lookup(param1) as ClientUISlotBarCategoryItemTimerStateModule;
            this.timerState.Read(param1, lookup);
            this.time = param1.ReadDouble();
            this.cooldownType = lookup.Lookup(param1) as CooldownTypeModule;
            this.cooldownType.Read(param1, lookup);
            this.maxTime = param1.ReadDouble();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.timerState.Write(param1);
            param1.WriteDouble(this.time);
            this.cooldownType.Write(param1);
            param1.WriteDouble(this.maxTime);
            param1.WriteShort(-8797);
        }
    }
}
