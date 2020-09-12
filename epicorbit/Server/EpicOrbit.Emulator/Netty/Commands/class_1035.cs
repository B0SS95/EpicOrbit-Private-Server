using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1035 : ICommand {

        public short ID { get; set; } = 8844;
        public OreCountModule var_824;

        public class_1035(OreCountModule param1 = null) {
            if (param1 == null) {
                this.var_824 = new OreCountModule();
            } else {
                this.var_824 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_824 = lookup.Lookup(param1) as OreCountModule;
            this.var_824.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_824.Write(param1);
            param1.WriteShort(10369);
        }
    }
}
