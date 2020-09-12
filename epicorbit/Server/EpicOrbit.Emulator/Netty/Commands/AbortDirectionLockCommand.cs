using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AbortDirectionLockCommand : ICommand {

        public short ID { get; set; } = 17974;
        public int uid = 0;

        public AbortDirectionLockCommand(int param1 = 0) {
            this.uid = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 13);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.uid, 19));
        }
    }
}
