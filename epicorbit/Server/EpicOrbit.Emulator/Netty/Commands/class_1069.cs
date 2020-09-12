using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1069 : ICommand {

        public short ID { get; set; } = 30003;
        public int name_99 = 0;

        public class_1069(int param1 = 0) {
            this.name_99 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_99 = param1.ReadInt();
            this.name_99 = param1.Shift(this.name_99, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.name_99, 29));
        }
    }
}
