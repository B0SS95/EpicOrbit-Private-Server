using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class StationModuleModule : ICommand {

        public const short NONE = 0;
        public const short DEFLECTOR = 3;
        public const short DAMAGE_BOOSTER = 11;
        public const short HONOR_BOOSTER = 10;
        public const short LASER_MID_RANGE = 6;
        public const short HULL = 2;
        public const short ROCKET_MID_ACCURACY = 8;
        public const short ROCKET_LOW_ACCURACY = 9;
        public const short LASER_LOW_RANGE = 7;
        public const short const_676 = 1;
        public const short REPAIR = 4;
        public const short EXPERIENCE_BOOSTER = 12;
        public const short LASER_HIGH_RANGE = 5;
        public short ID { get; set; } = 12852;
        public short type = 0;
        public int maxHitpoints = 0;
        public int itemId = 0;
        public int slotId = 0;
        public int maxShield = 0;
        public int emergencyRepairSecondsTotal = 0;
        public string ownerName = "";
        public int currentShield = 0;
        public int installationSecondsLeft = 0;
        public int upgradeLevel = 0;
        public int emergencyRepairSecondsLeft = 0;
        public int currentHitpoints = 0;
        public int emergencyRepairCost = 0;
        public int installationSeconds = 0;
        public int asteroidId = 0;

        public StationModuleModule(int param1 = 0, int param2 = 0, int param3 = 0, short param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, string param10 = "", int param11 = 0, int param12 = 0, int param13 = 0, int param14 = 0, int param15 = 0) {
            this.asteroidId = param1;
            this.itemId = param2;
            this.slotId = param3;
            this.type = param4;
            this.currentHitpoints = param5;
            this.maxHitpoints = param6;
            this.currentShield = param7;
            this.maxShield = param8;
            this.upgradeLevel = param9;
            this.ownerName = param10;
            this.installationSeconds = param11;
            this.installationSecondsLeft = param12;
            this.emergencyRepairSecondsLeft = param13;
            this.emergencyRepairSecondsTotal = param14;
            this.emergencyRepairCost = param15;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
            this.maxHitpoints = param1.ReadInt();
            this.maxHitpoints = param1.Shift(this.maxHitpoints, 29);
            this.itemId = param1.ReadInt();
            this.itemId = param1.Shift(this.itemId, 31);
            this.slotId = param1.ReadInt();
            this.slotId = param1.Shift(this.slotId, 13);
            this.maxShield = param1.ReadInt();
            this.maxShield = param1.Shift(this.maxShield, 21);
            this.emergencyRepairSecondsTotal = param1.ReadInt();
            this.emergencyRepairSecondsTotal = param1.Shift(this.emergencyRepairSecondsTotal, 25);
            this.ownerName = param1.ReadUTF();
            this.currentShield = param1.ReadInt();
            this.currentShield = param1.Shift(this.currentShield, 31);
            this.installationSecondsLeft = param1.ReadInt();
            this.installationSecondsLeft = param1.Shift(this.installationSecondsLeft, 19);
            this.upgradeLevel = param1.ReadInt();
            this.upgradeLevel = param1.Shift(this.upgradeLevel, 23);
            this.emergencyRepairSecondsLeft = param1.ReadInt();
            this.emergencyRepairSecondsLeft = param1.Shift(this.emergencyRepairSecondsLeft, 10);
            this.currentHitpoints = param1.ReadInt();
            this.currentHitpoints = param1.Shift(this.currentHitpoints, 27);
            this.emergencyRepairCost = param1.ReadInt();
            this.emergencyRepairCost = param1.Shift(this.emergencyRepairCost, 4);
            this.installationSeconds = param1.ReadInt();
            this.installationSeconds = param1.Shift(this.installationSeconds, 25);
            this.asteroidId = param1.ReadInt();
            this.asteroidId = param1.Shift(this.asteroidId, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
            param1.WriteInt(param1.Shift(this.maxHitpoints, 3));
            param1.WriteInt(param1.Shift(this.itemId, 1));
            param1.WriteInt(param1.Shift(this.slotId, 19));
            param1.WriteInt(param1.Shift(this.maxShield, 11));
            param1.WriteInt(param1.Shift(this.emergencyRepairSecondsTotal, 7));
            param1.WriteUTF(this.ownerName);
            param1.WriteInt(param1.Shift(this.currentShield, 1));
            param1.WriteInt(param1.Shift(this.installationSecondsLeft, 13));
            param1.WriteInt(param1.Shift(this.upgradeLevel, 9));
            param1.WriteInt(param1.Shift(this.emergencyRepairSecondsLeft, 22));
            param1.WriteInt(param1.Shift(this.currentHitpoints, 5));
            param1.WriteInt(param1.Shift(this.emergencyRepairCost, 28));
            param1.WriteInt(param1.Shift(this.installationSeconds, 7));
            param1.WriteInt(param1.Shift(this.asteroidId, 10));
        }
    }
}
