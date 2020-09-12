using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarCategoryItemModule : ICommand {

        public const short const_695 = 1;
        public const short SELECTION = 2;
        public const short const_3075 = 0;
        public const short TIMER = 3;
        public const short NUMBER = 1;
        public const short BAR = 2;
        public const short NONE = 0;
        public const short const_2067 = 3;
        public short ID { get; set; } = 19909;
        public int var_848 = 0;
        public bool showTooltipCooldownTimer = false;
        public ClientUISlotBarCategoryItemStatusModule status;
        public ClientUISlotBarCategoryItemTimerModule timer;
        public CooldownTypeModule var_1273;
        public short actionStyle = 0;
        public short counterType = 0;

        public ClientUISlotBarCategoryItemModule(int param1 = 0, ClientUISlotBarCategoryItemStatusModule param2 = null, ClientUISlotBarCategoryItemTimerModule param3 = null, CooldownTypeModule param4 = null, short param5 = 0, short param6 = 0, bool param7 = false) {
            this.var_848 = param1;
            if (param2 == null) {
                this.status = new ClientUISlotBarCategoryItemStatusModule();
            } else {
                this.status = param2;
            }
            if (param3 == null) {
                this.timer = new ClientUISlotBarCategoryItemTimerModule();
            } else {
                this.timer = param3;
            }
            if (param4 == null) {
                this.var_1273 = new CooldownTypeModule();
            } else {
                this.var_1273 = param4;
            }
            this.counterType = param5;
            this.actionStyle = param6;
            this.showTooltipCooldownTimer = param7;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_848 = param1.ReadInt();
            this.var_848 = param1.Shift(this.var_848, 31);
            this.showTooltipCooldownTimer = param1.ReadBoolean();
            this.status = lookup.Lookup(param1) as ClientUISlotBarCategoryItemStatusModule;
            this.status.Read(param1, lookup);
            this.timer = lookup.Lookup(param1) as ClientUISlotBarCategoryItemTimerModule;
            this.timer.Read(param1, lookup);
            this.var_1273 = lookup.Lookup(param1) as CooldownTypeModule;
            this.var_1273.Read(param1, lookup);
            this.actionStyle = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
            this.counterType = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_848, 1));
            param1.WriteBoolean(this.showTooltipCooldownTimer);
            this.status.Write(param1);
            this.timer.Write(param1);
            this.var_1273.Write(param1);
            param1.WriteShort(this.actionStyle);
            param1.WriteShort(27500);
            param1.WriteShort(23758);
            param1.WriteShort(this.counterType);
        }
    }
}
