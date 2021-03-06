using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_808 : ICommand {

        public short ID { get; set; } = 12727;
        public int windowId = 0;

        public class_808(int param1 = 0) {
            this.windowId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.windowId = param1.ReadInt();
            this.windowId = param1.Shift(this.windowId, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-3048);
            param1.WriteInt(param1.Shift(this.windowId, 2));
        }
    }
}
