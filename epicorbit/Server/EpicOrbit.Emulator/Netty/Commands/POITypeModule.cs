using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class POITypeModule : ICommand {

        public const short MINE_FIELD = 10;
        public const short CAGE = 9;
        public const short BUFF_ZONE = 11;
        public const short FACTION_NO_ACCESS = 6;
        public const short TRIGGER = 2;
        public const short NO_ACCESS = 5;
        public const short const_28 = 16;
        public const short const_891 = 18;
        public const short ENTER_LEAVE = 7;
        public const short const_743 = 15;
        public const short RADIATION = 8;
        public const short HEALING = 4;
        public const short const_70 = 17;
        public const short SECTOR_CONTROL_SECTOR_ZONE = 13;
        public const short GENERIC = 0;
        public const short FACTORIZED = 1;
        public const short const_2116 = 14;
        public const short DAMAGE = 3;
        public const short SECTOR_CONTROL_HOME_ZONE = 12;
        public short ID { get; set; } = 23864;
        public short typeValue = 0;

        public POITypeModule(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.typeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(27548);
            param1.WriteShort(this.typeValue);
        }
    }
}
