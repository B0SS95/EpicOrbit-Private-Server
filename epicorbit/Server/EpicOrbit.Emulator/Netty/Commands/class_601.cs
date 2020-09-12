using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_601 : ICommand {

        // Invitation?

        public short ID { get; set; } = 5513;
        public int to = 0;
        public int from = 0;

        public class_601(int param1 = 0, int param2 = 0) {
            this.from = param1;
            this.to = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.to = param1.ReadInt();
            this.to = param1.Shift(this.to, 3);
            this.from = param1.ReadInt();
            this.from = param1.Shift(this.from, 25);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-9622);
            param1.WriteInt(param1.Shift(this.to, 29));
            param1.WriteInt(param1.Shift(this.from, 7));
            param1.WriteShort(-27071);
        }
    }
}
