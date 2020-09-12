using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CooldownTypeModule : ICommand {

        public const short const_3023 = 43;
        public const short ROCKET_LAUNCHER = 11;
        public const short PLASMA = 7;
        public const short const_1137 = 8;
        public const short const_1783 = 27;
        public const short const_1412 = 26;
        public const short const_517 = 16;
        public const short const_491 = 20;
        public const short NONE = 0;
        public const short const_2528 = 28;
        public const short const_2234 = 17;
        public const short const_1303 = 44;
        public const short MINE = 1;
        public const short const_125 = 18;
        public const short const_1252 = 46;
        public const short const_1770 = 13;
        public const short const_1523 = 31;
        public const short const_386 = 47;
        public const short WIZARD = 5;
        public const short const_1158 = 22;
        public const short const_3290 = 32;
        public const short const_2110 = 6;
        public const short const_1116 = 29;
        public const short const_1316 = 41;
        public const short const_1244 = 36;
        public const short const_2219 = 10;
        public const short const_180 = 30;
        public const short const_1874 = 15;
        public const short const_233 = 9;
        public const short const_1949 = 39;
        public const short const_2253 = 33;
        public const short ROCKET = 4;
        public const short const_2163 = 23;
        public const short const_2686 = 38;
        public const short SPEED_BUFF = 35;
        public const short const_1368 = 19;
        public const short const_1801 = 3;
        public const short const_2996 = 45;
        public const short const_326 = 21;
        public const short const_409 = 24;
        public const short const_2732 = 37;
        public const short const_1008 = 12;
        public const short SINGULARITY = 34;
        public const short const_3403 = 25;
        public const short const_2910 = 42;
        public const short SMARTBOMB = 2;
        public const short const_2285 = 14;
        public const short const_2419 = 40;
        public short ID { get; set; } = 27745;
        public short typeValue = 0;

        public CooldownTypeModule(short param1 = 0) {
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
