using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionTypeModule : ICommand {

        public const short const_483 = 19;
        public const short FIREWORK_1 = 6;
        public const short X1 = 26;
        public const short const_128 = 18;
        public const short MINE_SL = 13;
        public const short DECELERATION = 38;
        public const short FIREWORK_3 = 8;
        public const short X2 = 27;
        public const short BDR1212 = 42;
        public const short MINE_EMP = 10;
        public const short RSB = 20;
        public const short FIREWORK_RZ = 44;
        public const short RB214 = 37;
        public const short EMP = 17;
        public const short PLASMA = 16;
        public const short MINE_DD = 12;
        public const short const_1801 = 15;
        public const short SAR01 = 24;
        public const short JOB100 = 41;
        public const short X4 = 29;
        public const short R310 = 32;
        public const short CBR = 39;
        public const short SAB = 30;
        public const short ROCKET = 0;
        public const short const_2941 = 40;
        public const short SMARTBOMB = 14;
        public const short PLT2021 = 34;
        public const short X3 = 28;
        public const short ECO_ROCKET = 23;
        public const short SAR02 = 25;
        public const short BDR1211 = 36;
        public const short PLT3030 = 35;
        public const short const_2638 = 1;
        public const short PLT2026 = 33;
        public const short FIREWORK_2 = 7;
        public const short WIZARD = 2;
        public const short UBER_ROCKET = 22;
        public const short const_1549 = 5;
        public const short FIREWORK_COM = 43;
        public const short MINE_SAB = 11;
        public const short CBO = 31;
        public const short const_2110 = 3;
        public const short MINE = 9;
        public const short const_423 = 4;
        public const short HELLSTORM = 21;
        public const short RIC3 = 45;
        public short ID { get; set; } = 18506;
        public short typeValue = 0;

        public AmmunitionTypeModule(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.typeValue);
            param1.WriteShort(6061);
        }
    }
}
