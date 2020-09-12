using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapAssetActionAvailableCommand : ICommand {

        public const short OFF = 1;
        public const short ON = 0;
        public short ID { get; set; } = 4644;
        public bool activatable = false;
        public int var_3378 = 0;
        public class_691 var_4611;
        public short state = 0;
        public ClientUITooltipsCommand toolTip;

        public MapAssetActionAvailableCommand(int param1 = 0, short param2 = 0, bool param3 = false, ClientUITooltipsCommand param4 = null, class_691 param5 = null) {
            this.var_3378 = param1;
            this.state = param2;
            this.activatable = param3;
            if (param4 == null) {
                this.toolTip = new ClientUITooltipsCommand();
            } else {
                this.toolTip = param4;
            }
            if (param5 == null) {
                this.var_4611 = new class_691();
            } else {
                this.var_4611 = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.activatable = param1.ReadBoolean();
            this.var_3378 = param1.ReadInt();
            this.var_3378 = param1.Shift(this.var_3378, 10);
            this.var_4611 = lookup.Lookup(param1) as class_691;
            this.var_4611.Read(param1, lookup);
            param1.ReadShort();
            this.state = param1.ReadShort();
            this.toolTip = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.toolTip.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.activatable);
            param1.WriteInt(param1.Shift(this.var_3378, 22));
            this.var_4611.Write(param1);
            param1.WriteShort(16300);
            param1.WriteShort(this.state);
            this.toolTip.Write(param1);
        }
    }
}
