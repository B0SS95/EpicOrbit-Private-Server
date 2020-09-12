using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class TechTypeModule : ICommand {

        public const short ROCKET_PROBABILITY_MAXIMIZER = 2;
        public const short CLINGING_IMPULSE_DRONE = 6;
        public const short ENERGY_LEECH_ARRAY = 0;
        public const short ENERGY_CHAIN_IMPULSE = 1;
        public const short SHIELD_BACKUP = 3;
        public const short SPEED_LEECH = 4;
        public const short BATTLE_REPBOT = 5;
        public short ID { get; set; } = 51;
        public short typeValue = 0;

        public TechTypeModule(short param1 = 0) {
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
