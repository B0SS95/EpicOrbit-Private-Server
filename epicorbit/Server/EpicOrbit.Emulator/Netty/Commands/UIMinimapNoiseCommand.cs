using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIMinimapNoiseCommand : ICommand {

        public short ID { get; set; } = 21325;
        public int duration = 0;

        public UIMinimapNoiseCommand(int param1 = 0) {
            this.duration = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.duration = param1.ReadInt();
            this.duration = param1.Shift(this.duration, 11);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.duration, 21));
            param1.WriteShort(1349);
        }
    }
}
