using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class OreTypeModule : ICommand {

        public const short PALLADIUM = 8;
        public const short PRISMATIUM = 12;
        public const short ENDURIUM = 1;
        public const short PROMETID = 4;
        public const short SEPROM = 7;
        public const short PROMERIUM = 6;
        public const short TERBIUM = 2;
        public const short QUADROTHRIN = 16;
        public const short DURANIUM = 5;
        public const short TETRATHRIN = 18;
        public const short TRITTOTHRIN = 15;
        public const short BOLTRUM = 10;
        public const short const_2788 = 13;
        public const short BIFENON = 19;
        public const short SCRAPIUM = 11;
        public const short XENOMIT = 3;
        public const short DUOTHRIN = 14;
        public const short INDOCTRINEOIL = 20;
        public const short PROMETIUM = 0;
        public const short KYHALON = 17;
        public const short MUCOSUM = 9;
        public const short HYBRID_ALLOY = 21;
        public short ID { get; set; } = 4539;
        public short typeValue = 0;

        public OreTypeModule(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.typeValue);
        }
    }
}
