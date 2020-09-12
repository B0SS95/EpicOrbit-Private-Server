using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ButtonIDModule : ICommand {

        public const short LASER_x1 = 0;
        public const short LASER_x2 = 1;
        public const short LASER_x3 = 2;
        public const short LASER_x4 = 3;
        public const short LASER_SAB = 4;
        public const short LASER_RSB = 5;
        public const short ROCKET_1 = 6;
        public const short ROCKET_2 = 7;
        public const short ROCKET_3 = 8;
        public const short WIZARD = 9;
        public const short PLASMA = 10;
        public const short DECELERATION_ROCKET = 11;
        public const short EMP = 12;
        public const short MINE_ACM = 13;
        public const short MINE_EMP = 14;
        public const short MINE_SAB = 15;
        public const short MINE_DD = 16;
        public const short ROCKET_LAUNCHER = 17;
        public const short HELLSTORM_01 = 18;
        public const short UBR_100 = 19;
        public const short ECO_10 = 20;
        public const short CPU_DRONE_REPAIR = 21;
        public const short CPU_AIM = 22;
        public const short CPU_AROL = 23;
        public const short CPU_CLOAK = 24;
        public const short CPU_JUMP = 25;
        public const short CPU_REPAIR_ROBOT = 26;
        public const short CPU_HM7 = 27;
        public const short CPU_AMMOBUY = 28;
        public const short JUMP = 29;
        public const short FAST_REPAIR = 30;
        public const short LOGOUT = 31;
        public const short MENU_LASER = 32;
        public const short MENU_ROCKET = 33;
        public const short MENU_EXPLOSIVES = 34;
        public const short MENU_CPU = 35;
        public const short MENU_EXTRAS = 36;
        public const short MENU_TECHS = 37;
        public const short MENU_SKILLS = 38;

        public short ID { get; set; } = 29295;
        public short idValue = 0;

        public ButtonIDModule(short param1 = 0) {
            this.idValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.idValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-4156);
            param1.WriteShort(this.idValue);
            param1.WriteShort(-9975);
        }
    }
}
