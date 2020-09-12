using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_792 : ICommand {

        public short ID { get; set; } = 23545;

        public class_792() {
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-32352);
            param1.WriteShort(12866);
        }
    }
}
