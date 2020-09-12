using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackTypeModule : ICommand {

        public const short SL = 6;
        public const short const_1864 = 15;
        public const short RADIATION = 3;
        public const short REPAIR = 10;
        public const short const_298 = 12;
        public const short const_499 = 13;
        public const short LASER = 1;
        public const short ROCKET = 0;
        public const short DECELERATION = 11;
        public const short PLASMA = 4;
        public const short SINGULARITY = 8;
        public const short ECI = 5;
        public const short MINE = 2;
        public const short SMARTBOMB = 14;
        public const short KAMIKAZE = 9;
        public const short CID = 7;
        public short ID { get; set; } = 16440;
        public short attackTypeValue = 0;

        public AttackTypeModule(short param1 = 0) {
            this.attackTypeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.attackTypeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.attackTypeValue);
        }
    }
}
