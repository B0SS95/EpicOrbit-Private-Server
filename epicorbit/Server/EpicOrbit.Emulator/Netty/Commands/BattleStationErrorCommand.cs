using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BattleStationErrorCommand : ICommand {

        public const short CONCURRENT_EQUIP = 5;
        public const short ITEM_NOT_IN_STATION = 12;
        public const short ITEM_HITPOINTS_ZERO = 3;
        public const short REPLACE_RIGHT_MISSING = 6;
        public const short REPAIR_NO_MONEY = 17;
        public const short ITEM_NOT_OWNED = 7;
        public const short OUT_OF_RANGE = 8;
        public const short DEFLECTOR_ALREADY_OFF = 15;
        public const short EQUIP_OF_SAME_PLAYER_RUNNING = 9;
        public const short MAX_NUMBER_OF_MODULE_TYPE_EXCEEDED = 13;
        public const short REPAIR_NO_MODULE = 16;
        public const short NO_CLAN = 1;
        public const short ITEM_ALREADY_EQUIPPED_IN_ANOTHER_ASTEROID = 4;
        public const short REPAIR_ALREADY_RUNNING = 18;
        public const short UNSPECIFIED = 0;
        public const short DEFLECTOR_NO_RIGHTS = 14;
        public const short HUB_ONLY = 11;
        public const short STATION_ALREADY_BUILDING = 2;
        public const short SATELLITE_ONLY = 10;
        public short ID { get; set; } = 28964;
        public short type = 0;

        public BattleStationErrorCommand(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
        }
    }
}
