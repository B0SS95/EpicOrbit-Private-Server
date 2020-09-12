using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PriceModule : ICommand {

        public const short CREDITS = 0;
        public const short URIDIUM = 1;
        public short ID { get; set; } = 8621;
        public short type = 0;
        public int amount = 0;

        public PriceModule(short param1 = 0, int param2 = 0) {
            this.type = param1;
            this.amount = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 15);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
            param1.WriteInt(param1.Shift(this.amount, 17));
            param1.WriteShort(20811);
            param1.WriteShort(-12067);
        }
    }
}
