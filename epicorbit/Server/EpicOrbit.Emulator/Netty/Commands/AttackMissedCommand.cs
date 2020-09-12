using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackMissedCommand : ICommand {

        public short ID { get; set; } = 31565;
        public int skillColorId = 0;
        public int targetUserId = 0;
        public AttackTypeModule attackType;

        public AttackMissedCommand(AttackTypeModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.attackType = new AttackTypeModule();
            } else {
                this.attackType = param1;
            }
            this.targetUserId = param2;
            this.skillColorId = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.skillColorId = param1.ReadInt();
            this.skillColorId = param1.Shift(this.skillColorId, 16);
            this.targetUserId = param1.ReadInt();
            this.targetUserId = param1.Shift(this.targetUserId, 11);
            this.attackType = lookup.Lookup(param1) as AttackTypeModule;
            this.attackType.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.skillColorId, 16));
            param1.WriteInt(param1.Shift(this.targetUserId, 21));
            this.attackType.Write(param1);
        }
    }
}
