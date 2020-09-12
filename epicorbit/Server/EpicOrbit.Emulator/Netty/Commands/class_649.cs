using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_649 : ICommand {

        public short ID { get; set; } = 27088;
        public int var_2896 = 0;
        public ClientUITooltipsCommand toolTip;
        public int var_1876 = 0;

        public class_649(int param1 = 0, int param2 = 0, ClientUITooltipsCommand param3 = null) {
            this.var_1876 = param1;
            this.var_2896 = param2;
            if (param3 == null) {
                this.toolTip = new ClientUITooltipsCommand();
            } else {
                this.toolTip = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2896 = param1.ReadInt();
            this.var_2896 = param1.Shift(this.var_2896, 16);
            this.toolTip = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.toolTip.Read(param1, lookup);
            this.var_1876 = param1.ReadInt();
            this.var_1876 = param1.Shift(this.var_1876, 19);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-1251);
            param1.WriteInt(param1.Shift(this.var_2896, 16));
            this.toolTip.Write(param1);
            param1.WriteInt(param1.Shift(this.var_1876, 13));
            param1.WriteShort(28970);
        }
    }
}
