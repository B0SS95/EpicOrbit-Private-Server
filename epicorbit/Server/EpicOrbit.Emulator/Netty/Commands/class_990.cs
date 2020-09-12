using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_990 : ClientUIMenuBarItemModule, ICommand {

        public override short ID { get; set; } = 1713;
        public bool maximized = false;
        public int height = 0;
        public string var_1759 = "";
        public int width = 0;
        public int var_2374 = 0;
        public int var_514 = 0;
        public ClientUITooltipsCommand helpText;

        public class_990(bool param1 = false, int param2 = 0, bool param3 = false, int param4 = 0, int param5 = 0, ClientUITooltipsCommand param6 = null, string param7 = "", int param8 = 0, ClientUITooltipsCommand param9 = null, string param10 = "")
         : base(param10, param3, param6) {
            this.var_1759 = param7;
            this.var_514 = param5;
            this.var_2374 = param4;
            this.width = param8;
            this.height = param2;
            this.maximized = param1;
            if (param9 == null) {
                this.helpText = new ClientUITooltipsCommand();
            } else {
                this.helpText = param9;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.maximized = param1.ReadBoolean();
            this.height = param1.ReadInt();
            this.height = param1.Shift(this.height, 9);
            this.var_1759 = param1.ReadUTF();
            this.width = param1.ReadInt();
            this.width = param1.Shift(this.width, 13);
            this.var_2374 = param1.ReadInt();
            this.var_2374 = param1.Shift(this.var_2374, 21);
            this.var_514 = param1.ReadInt();
            this.var_514 = param1.Shift(this.var_514, 11);
            this.helpText = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.helpText.Read(param1, lookup);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteBoolean(this.maximized);
            param1.WriteInt(param1.Shift(this.height, 23));
            param1.WriteUTF(this.var_1759);
            param1.WriteInt(param1.Shift(this.width, 19));
            param1.WriteInt(param1.Shift(this.var_2374, 11));
            param1.WriteInt(param1.Shift(this.var_514, 21));
            this.helpText.Write(param1);
            param1.WriteShort(25900);
        }
    }
}
