using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SpaceBallUpdateSpeedCommand : ICommand {

        public short ID { get; set; } = 28283;
        public int factionId = 0;
        public int speed = 0;

        public SpaceBallUpdateSpeedCommand(int param1 = 0, int param2 = 0) {
            this.factionId = param1;
            this.speed = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 20);
            this.speed = param1.ReadInt();
            this.speed = param1.Shift(this.speed, 31);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.factionId, 12));
            param1.WriteInt(param1.Shift(this.speed, 1));
            param1.WriteShort(12841);
            param1.WriteShort(14688);
        }
    }
}
