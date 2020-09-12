using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class POIDesignModule : ICommand {

        public const short NEBULA = 5;
        public const short const_743 = 10;
        public const short SECTOR_CONTROL_SECTOR_ZONE = 8;
        public const short SECTOR_CONTROL_HOME_ZONE = 7;
        public const short const_70 = 11;
        public const short NONE = 0;
        public const short ASTEROIDS_BLUE = 2;
        public const short const_1649 = 13;
        public const short SCRAP = 4;
        public const short SIMPLE = 6;
        public const short ASTEROIDS_MIXED_WITH_SCRAP = 3;
        public const short ASTEROIDS = 1;
        public const short const_891 = 14;
        public const short const_256 = 12;
        public const short const_2116 = 9;
        public short ID { get; set; } = 18787;
        public short designValue = 0;

        public POIDesignModule(short param1 = 0) {
            this.designValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.designValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.designValue);
        }
    }
}
