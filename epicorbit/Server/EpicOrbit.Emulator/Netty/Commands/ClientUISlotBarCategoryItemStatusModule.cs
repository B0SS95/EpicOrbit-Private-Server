using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarCategoryItemStatusModule : ICommand {

        public const short RED = 1;
        public const short YELLOW = 3;
        public const short BLUE = 4;
        public const short GREEN = 2;
        public const short const_998 = 6;
        public const short const_1461 = 5;
        public const short DEFAULT = 0;
        public short ID { get; set; } = 11107;
        public bool activatable = false;
        public bool blocked = false;
        public short counterStyle = 0;
        public bool selected = false;
        public ClientUITooltipsCommand toolTipSlotBar;
        public bool buyable = false;
        public ClientUITooltipsCommand toolTipItemBar;
        public double counterValue = 0;
        public bool visible = false;
        public string iconLootId = "";
        public bool available = false;
        public double maxCounterValue = 0;
        public string var_2176 = "";

        public ClientUISlotBarCategoryItemStatusModule(bool param1 = false, bool param2 = false, string param3 = "", ClientUITooltipsCommand param4 = null, ClientUITooltipsCommand param5 = null, bool param6 = false, double param7 = 0, double param8 = 0, short param9 = 0, string param10 = "", bool param11 = false, bool param12 = false, bool param13 = false) {
            this.available = param1;
            this.visible = param2;
            this.var_2176 = param3;
            if (param4 == null) {
                this.toolTipItemBar = new ClientUITooltipsCommand();
            } else {
                this.toolTipItemBar = param4;
            }
            if (param5 == null) {
                this.toolTipSlotBar = new ClientUITooltipsCommand();
            } else {
                this.toolTipSlotBar = param5;
            }
            this.buyable = param6;
            this.maxCounterValue = param7;
            this.counterValue = param8;
            this.counterStyle = param9;
            this.iconLootId = param10;
            this.activatable = param11;
            this.selected = param12;
            this.blocked = param13;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.activatable = param1.ReadBoolean();
            this.blocked = param1.ReadBoolean();
            this.counterStyle = param1.ReadShort();
            this.selected = param1.ReadBoolean();
            param1.ReadShort();
            this.toolTipSlotBar = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.toolTipSlotBar.Read(param1, lookup);
            this.buyable = param1.ReadBoolean();
            this.toolTipItemBar = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.toolTipItemBar.Read(param1, lookup);
            this.counterValue = param1.ReadDouble();
            this.visible = param1.ReadBoolean();
            this.iconLootId = param1.ReadUTF();
            this.available = param1.ReadBoolean();
            this.maxCounterValue = param1.ReadDouble();
            this.var_2176 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.activatable);
            param1.WriteBoolean(this.blocked);
            param1.WriteShort(this.counterStyle);
            param1.WriteBoolean(this.selected);
            param1.WriteShort(6376);
            this.toolTipSlotBar.Write(param1);
            param1.WriteBoolean(this.buyable);
            this.toolTipItemBar.Write(param1);
            param1.WriteDouble(this.counterValue);
            param1.WriteBoolean(this.visible);
            param1.WriteUTF(this.iconLootId);
            param1.WriteBoolean(this.available);
            param1.WriteDouble(this.maxCounterValue);
            param1.WriteUTF(this.var_2176);
        }
    }
}
