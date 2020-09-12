using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class OrePriceModule : ICommand {

        public short ID { get; set; } = 17099;
        public OreTypeModule oreType;
        public int price = 0;

        public OrePriceModule(OreTypeModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.oreType = new OreTypeModule();
            } else {
                this.oreType = param1;
            }
            this.price = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.oreType = lookup.Lookup(param1) as OreTypeModule;
            this.oreType.Read(param1, lookup);
            this.price = param1.ReadInt();
            this.price = param1.Shift(this.price, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.oreType.Write(param1);
            param1.WriteInt(param1.Shift(this.price, 15));
        }
    }
}
