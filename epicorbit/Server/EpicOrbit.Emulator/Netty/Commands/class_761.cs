using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_761 : ICommand {

        public short ID { get; set; } = 25737;
        public int seconds = 0;

        public class_761(int param1 = 0) {
            this.seconds = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.seconds = param1.ReadInt();
            this.seconds = param1.Shift(this.seconds, 9);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.seconds, 23));
            param1.WriteShort(31893);
        }
    }
}
