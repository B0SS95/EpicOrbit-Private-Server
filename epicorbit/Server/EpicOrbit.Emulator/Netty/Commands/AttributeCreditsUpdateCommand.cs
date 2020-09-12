using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeCreditsUpdateCommand : ICommand {

        public short ID { get; set; } = 12911;
        public int uridium = 0;
        public float jackpot = 0;
        public int credits = 0;

        public AttributeCreditsUpdateCommand(int param1 = 0, int param2 = 0, float param3 = 0) {
            this.credits = param1;
            this.uridium = param2;
            this.jackpot = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.uridium = param1.ReadInt();
            this.uridium = param1.Shift(this.uridium, 12);
            this.jackpot = param1.ReadFloat();
            this.credits = param1.ReadInt();
            this.credits = param1.Shift(this.credits, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.uridium, 20));
            param1.WriteFloat(this.jackpot);
            param1.WriteInt(param1.Shift(this.credits, 29));
        }
    }
}
