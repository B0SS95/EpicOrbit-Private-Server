using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BoostedAttributeTypeModule : ICommand {

        public const int EP = 0;
        public const int SHIELDRECHARGE = 5;
        public const int BONUSBOXES = 9;
        public const int RESOURCE = 6;
        public const int MAXHP = 7;
        public const int const_1831 = 12;
        public const int REPAIR = 4;
        public const int HONOUR = 1;
        public const int ABILITY_COOLDOWN = 8;
        public const int CHANCE = 11;
        public const int DAMAGE = 2;
        public const int QUESTREWARD = 10;
        public const int SHIELD = 3;

        public short ID { get; set; } = 7512;
        public short typeValue = 0;

        public BoostedAttributeTypeModule(short param1 = 0) {
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
