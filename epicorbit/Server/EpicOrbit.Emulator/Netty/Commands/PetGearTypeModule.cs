using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetGearTypeModule : ICommand {

        public const short KAMIKAZE = 10;
        public const short TRADE_POD = 8;
        public const short ENEMY_LOCATOR = 6;
        public const short AUTO_LOOT = 4;
        public const short AUTO_RESOURCE_COLLECTION = 5;
        public const short BEHAVIOR = 0;
        public const short FRIENDLY_SACRIFICE = 14;
        public const short REPAIR_PET = 9;
        public const short GEAR = 3;
        public const short GUARD = 2;
        public const short COMBO_GUARD = 12;
        public const short PASSIVE = 1;
        public const short ADMIN = 13;
        public const short COMBO_SHIP_REPAIR = 11;
        public const short RESOURCE_LOCATOR = 7;
        public short ID { get; set; } = 4378;
        public short typeValue = 0;

        public PetGearTypeModule(short param1 = 0) {
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
