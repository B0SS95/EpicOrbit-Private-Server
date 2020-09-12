using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackHitCommand : ICommand {

        public short ID { get; set; } = 9461;
        public int damage = 0;
        public bool skilled = false;
        public int victimId = 0;
        public int victimHitpoints = 0;
        public int victimNanoHull = 0;
        public int attackerId = 0;
        public AttackTypeModule attackType;
        public int victimShield = 0;

        public AttackHitCommand(AttackTypeModule param1 = null, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, bool param8 = false) {
            if (param1 == null) {
                this.attackType = new AttackTypeModule();
            } else {
                this.attackType = param1;
            }
            this.attackerId = param2;
            this.victimId = param3;
            this.victimHitpoints = param4;
            this.victimShield = param5;
            this.victimNanoHull = param6;
            this.damage = param7;
            this.skilled = param8;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.damage = param1.ReadInt();
            this.damage = param1.Shift(this.damage, 17);
            this.skilled = param1.ReadBoolean();
            this.victimId = param1.ReadInt();
            this.victimId = param1.Shift(this.victimId, 28);
            this.victimHitpoints = param1.ReadInt();
            this.victimHitpoints = param1.Shift(this.victimHitpoints, 25);
            this.victimNanoHull = param1.ReadInt();
            this.victimNanoHull = param1.Shift(this.victimNanoHull, 16);
            this.attackerId = param1.ReadInt();
            this.attackerId = param1.Shift(this.attackerId, 18);
            this.attackType = lookup.Lookup(param1) as AttackTypeModule;
            this.attackType.Read(param1, lookup);
            this.victimShield = param1.ReadInt();
            this.victimShield = param1.Shift(this.victimShield, 2);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.damage, 15));
            param1.WriteBoolean(this.skilled);
            param1.WriteInt(param1.Shift(this.victimId, 4));
            param1.WriteInt(param1.Shift(this.victimHitpoints, 7));
            param1.WriteInt(param1.Shift(this.victimNanoHull, 16));
            param1.WriteInt(param1.Shift(this.attackerId, 14));
            this.attackType.Write(param1);
            param1.WriteInt(param1.Shift(this.victimShield, 30));
        }
    }
}
