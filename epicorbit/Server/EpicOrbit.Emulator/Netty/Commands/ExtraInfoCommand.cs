using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ExtraInfoCommand : ICommand {

        public short ID { get; set; } = 1509;
        public int smartBomb = 0;
        public int jumpCPU = 0;
        public int mineTurbo = 0;
        public int droneCPU = 0;
        public int cloakCPU = 0;
        public int specialJumpCPU = 0;
        public int rocketBuyCPU = 0;
        public int autoRlCPU = 0;
        public int ammoCPU = 0;
        public int diploCPU = 0;
        public int arolCPU = 0;
        public int instaShield = 0;
        public int aimCPU = 0;

        public ExtraInfoCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, int param11 = 0, int param12 = 0, int param13 = 0) {
            this.droneCPU = param1;
            this.diploCPU = param2;
            this.jumpCPU = param3;
            this.ammoCPU = param4;
            this.smartBomb = param5;
            this.instaShield = param6;
            this.mineTurbo = param7;
            this.aimCPU = param8;
            this.arolCPU = param9;
            this.cloakCPU = param10;
            this.autoRlCPU = param11;
            this.rocketBuyCPU = param12;
            this.specialJumpCPU = param13;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.smartBomb = param1.ReadInt();
            this.smartBomb = param1.Shift(this.smartBomb, 12);
            this.jumpCPU = param1.ReadInt();
            this.jumpCPU = param1.Shift(this.jumpCPU, 3);
            this.mineTurbo = param1.ReadInt();
            this.mineTurbo = param1.Shift(this.mineTurbo, 1);
            this.droneCPU = param1.ReadInt();
            this.droneCPU = param1.Shift(this.droneCPU, 10);
            this.cloakCPU = param1.ReadInt();
            this.cloakCPU = param1.Shift(this.cloakCPU, 8);
            this.specialJumpCPU = param1.ReadInt();
            this.specialJumpCPU = param1.Shift(this.specialJumpCPU, 21);
            this.rocketBuyCPU = param1.ReadInt();
            this.rocketBuyCPU = param1.Shift(this.rocketBuyCPU, 25);
            this.autoRlCPU = param1.ReadInt();
            this.autoRlCPU = param1.Shift(this.autoRlCPU, 7);
            this.ammoCPU = param1.ReadInt();
            this.ammoCPU = param1.Shift(this.ammoCPU, 26);
            this.diploCPU = param1.ReadInt();
            this.diploCPU = param1.Shift(this.diploCPU, 5);
            this.arolCPU = param1.ReadInt();
            this.arolCPU = param1.Shift(this.arolCPU, 18);
            this.instaShield = param1.ReadInt();
            this.instaShield = param1.Shift(this.instaShield, 11);
            this.aimCPU = param1.ReadInt();
            this.aimCPU = param1.Shift(this.aimCPU, 20);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.smartBomb, 20));
            param1.WriteInt(param1.Shift(this.jumpCPU, 29));
            param1.WriteInt(param1.Shift(this.mineTurbo, 31));
            param1.WriteInt(param1.Shift(this.droneCPU, 22));
            param1.WriteInt(param1.Shift(this.cloakCPU, 24));
            param1.WriteInt(param1.Shift(this.specialJumpCPU, 11));
            param1.WriteInt(param1.Shift(this.rocketBuyCPU, 7));
            param1.WriteInt(param1.Shift(this.autoRlCPU, 25));
            param1.WriteInt(param1.Shift(this.ammoCPU, 6));
            param1.WriteInt(param1.Shift(this.diploCPU, 27));
            param1.WriteInt(param1.Shift(this.arolCPU, 14));
            param1.WriteInt(param1.Shift(this.instaShield, 21));
            param1.WriteInt(param1.Shift(this.aimCPU, 12));
        }
    }
}
