using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipSelectionCommand : ICommand {

        public short ID { get; set; } = 11261;
        public int hitpointsMax = 0;
        public int maxNanoHull = 0;
        public int shipType = 0;
        public int hitpoints = 0;
        public int shieldMax = 0;
        public int nanoHull = 0;
        public int shield = 0;
        public int userId = 0;
        public bool shieldSkill = false;

        public ShipSelectionCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, bool param9 = false) {
            this.userId = param1;
            this.shipType = param2;
            this.shield = param3;
            this.shieldMax = param4;
            this.hitpoints = param5;
            this.hitpointsMax = param6;
            this.nanoHull = param7;
            this.maxNanoHull = param8;
            this.shieldSkill = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hitpointsMax = param1.ReadInt();
            this.hitpointsMax = param1.Shift(this.hitpointsMax, 4);
            this.maxNanoHull = param1.ReadInt();
            this.maxNanoHull = param1.Shift(this.maxNanoHull, 31);
            this.shipType = param1.ReadInt();
            this.shipType = param1.Shift(this.shipType, 6);
            this.hitpoints = param1.ReadInt();
            this.hitpoints = param1.Shift(this.hitpoints, 25);
            this.shieldMax = param1.ReadInt();
            this.shieldMax = param1.Shift(this.shieldMax, 22);
            this.nanoHull = param1.ReadInt();
            this.nanoHull = param1.Shift(this.nanoHull, 20);
            param1.ReadShort();
            this.shield = param1.ReadInt();
            this.shield = param1.Shift(this.shield, 27);
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 6);
            this.shieldSkill = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.hitpointsMax, 28));
            param1.WriteInt(param1.Shift(this.maxNanoHull, 1));
            param1.WriteInt(param1.Shift(this.shipType, 26));
            param1.WriteInt(param1.Shift(this.hitpoints, 7));
            param1.WriteInt(param1.Shift(this.shieldMax, 10));
            param1.WriteInt(param1.Shift(this.nanoHull, 12));
            param1.WriteShort(-6746);
            param1.WriteInt(param1.Shift(this.shield, 5));
            param1.WriteShort(-9195);
            param1.WriteInt(param1.Shift(this.userId, 26));
            param1.WriteBoolean(this.shieldSkill);
        }
    }
}
