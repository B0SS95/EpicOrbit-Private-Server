using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_764 : ICommand {

        // SectorControlDraftConfirmationCommand

        public short ID { get; set; } = 7905;
        public int seconds = 0;

        public class_764(int param1 = 0) {
            this.seconds = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.seconds = param1.ReadInt();
            this.seconds = param1.Shift(this.seconds, 10);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-26536);
            param1.WriteInt(param1.Shift(this.seconds, 22));
        }
    }
}
