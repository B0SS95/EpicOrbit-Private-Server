using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_554 : ICommand {

        // LabRefinementRequest
        // TradeSellOreRequest

        public short ID { get; set; } = 165;
        public OreCountModule var_4838;

        public class_554(OreCountModule param1 = null) {
            if (param1 == null) {
                this.var_4838 = new OreCountModule();
            } else {
                this.var_4838 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_4838 = lookup.Lookup(param1) as OreCountModule;
            this.var_4838.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(577);
            this.var_4838.Write(param1);
        }
    }
}
