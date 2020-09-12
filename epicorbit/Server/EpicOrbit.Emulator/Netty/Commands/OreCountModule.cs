using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class OreCountModule : ICommand {

        public short ID { get; set; } = 15781;
        public OreTypeModule oreType;
        public double count = 0;

        public OreCountModule(OreTypeModule param1 = null, double param2 = 0) {
            if (param1 == null) {
                this.oreType = new OreTypeModule();
            } else {
                this.oreType = param1;
            }
            this.count = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.oreType = lookup.Lookup(param1) as OreTypeModule;
            this.oreType.Read(param1, lookup);
            param1.ReadShort();
            param1.ReadShort();
            this.count = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.oreType.Write(param1);
            param1.WriteShort(-30269);
            param1.WriteShort(11228);
            param1.WriteDouble(this.count);
        }
    }
}
