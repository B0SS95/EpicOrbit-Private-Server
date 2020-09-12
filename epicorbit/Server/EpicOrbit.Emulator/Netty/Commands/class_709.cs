using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_709 : ICommand {

        public short ID { get; set; } = 28486;
        public int name_67 = 0;

        public class_709(int param1 = 0) {
            this.name_67 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_67 = param1.ReadInt();
            this.name_67 = param1.Shift(this.name_67, 13);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.name_67, 19));
        }
    }
}
