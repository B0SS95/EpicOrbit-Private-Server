using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_917 : ICommand {

        public short ID { get; set; } = 9198;

        public class_917() {
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(6515);
        }
    }
}
