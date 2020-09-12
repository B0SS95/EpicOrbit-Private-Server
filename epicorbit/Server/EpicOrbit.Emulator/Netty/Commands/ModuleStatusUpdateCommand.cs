using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ModuleStatusUpdateCommand : ICommand {

        public short ID { get; set; } = 29485;
        public int shield = 0;
        public int hitpoints = 0;
        public int asteroidId = 0;
        public int emergencyRepairSecondsTotal = 0;
        public int slotId = 0;
        public int shieldMax = 0;
        public int emergencyRepairSecondsLeft = 0;
        public int hitpointsMax = 0;

        public ModuleStatusUpdateCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0) {
            this.asteroidId = param1;
            this.slotId = param2;
            this.hitpoints = param3;
            this.hitpointsMax = param4;
            this.shield = param5;
            this.shieldMax = param6;
            this.emergencyRepairSecondsLeft = param7;
            this.emergencyRepairSecondsTotal = param8;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.shield = param1.ReadInt();
            this.shield = param1.Shift(this.shield, 27);
            this.hitpoints = param1.ReadInt();
            this.hitpoints = param1.Shift(this.hitpoints, 2);
            this.asteroidId = param1.ReadInt();
            this.asteroidId = param1.Shift(this.asteroidId, 27);
            this.emergencyRepairSecondsTotal = param1.ReadInt();
            this.emergencyRepairSecondsTotal = param1.Shift(this.emergencyRepairSecondsTotal, 1);
            this.slotId = param1.ReadInt();
            this.slotId = param1.Shift(this.slotId, 29);
            this.shieldMax = param1.ReadInt();
            this.shieldMax = param1.Shift(this.shieldMax, 12);
            this.emergencyRepairSecondsLeft = param1.ReadInt();
            this.emergencyRepairSecondsLeft = param1.Shift(this.emergencyRepairSecondsLeft, 16);
            this.hitpointsMax = param1.ReadInt();
            this.hitpointsMax = param1.Shift(this.hitpointsMax, 6);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.shield, 5));
            param1.WriteInt(param1.Shift(this.hitpoints, 30));
            param1.WriteInt(param1.Shift(this.asteroidId, 5));
            param1.WriteInt(param1.Shift(this.emergencyRepairSecondsTotal, 31));
            param1.WriteInt(param1.Shift(this.slotId, 3));
            param1.WriteInt(param1.Shift(this.shieldMax, 20));
            param1.WriteInt(param1.Shift(this.emergencyRepairSecondsLeft, 16));
            param1.WriteInt(param1.Shift(this.hitpointsMax, 26));
        }
    }
}
