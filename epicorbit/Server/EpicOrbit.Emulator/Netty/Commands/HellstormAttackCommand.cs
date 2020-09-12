using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HellstormAttackCommand : ICommand {

        public short ID { get; set; } = 11482;
        public bool hit = false;
        public int targetId = 0;
        public int currentLoad = 0;
        public int attackerId = 0;
        public AmmunitionTypeModule rocketType;

        public HellstormAttackCommand(int param1 = 0, int param2 = 0, bool param3 = false, int param4 = 0, AmmunitionTypeModule param5 = null) {
            this.attackerId = param1;
            this.targetId = param2;
            this.hit = param3;
            this.currentLoad = param4;
            if (param5 == null) {
                this.rocketType = new AmmunitionTypeModule();
            } else {
                this.rocketType = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hit = param1.ReadBoolean();
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 28);
            this.currentLoad = param1.ReadInt();
            this.currentLoad = param1.Shift(this.currentLoad, 6);
            this.attackerId = param1.ReadInt();
            this.attackerId = param1.Shift(this.attackerId, 21);
            this.rocketType = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.rocketType.Read(param1, lookup);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.hit);
            param1.WriteInt(param1.Shift(this.targetId, 4));
            param1.WriteInt(param1.Shift(this.currentLoad, 26));
            param1.WriteInt(param1.Shift(this.attackerId, 11));
            this.rocketType.Write(param1);
            param1.WriteShort(3310);
            param1.WriteShort(29209);
        }
    }
}
