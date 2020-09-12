using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_939 : ICommand {

        public short ID { get; set; } = 32326;
        public double itemId = 0;

        public class_939(double param1 = 0) {
            this.itemId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemId = param1.ReadDouble();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.itemId);
            param1.WriteShort(-4337);
            param1.WriteShort(8144);
        }
    }
}
