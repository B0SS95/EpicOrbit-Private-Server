using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HealCommand : ICommand {

        public const short SHIELD = 1;
        public const short HITPOINTS = 0;
        public short ID { get; set; } = 371;
        public int healAmount = 0;
        public int healedId = 0;
        public short healType = 0;
        public int currentHitpoints = 0;
        public int healerId = 0;

        public HealCommand(short param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.healType = param1;
            this.healerId = param2;
            this.healedId = param3;
            this.currentHitpoints = param4;
            this.healAmount = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.healAmount = param1.ReadInt();
            this.healAmount = param1.Shift(this.healAmount, 30);
            this.healedId = param1.ReadInt();
            this.healedId = param1.Shift(this.healedId, 24);
            this.healType = param1.ReadShort();
            this.currentHitpoints = param1.ReadInt();
            this.currentHitpoints = param1.Shift(this.currentHitpoints, 31);
            this.healerId = param1.ReadInt();
            this.healerId = param1.Shift(this.healerId, 29);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.healAmount, 2));
            param1.WriteInt(param1.Shift(this.healedId, 8));
            param1.WriteShort(this.healType);
            param1.WriteInt(param1.Shift(this.currentHitpoints, 1));
            param1.WriteInt(param1.Shift(this.healerId, 3));
        }
    }
}
