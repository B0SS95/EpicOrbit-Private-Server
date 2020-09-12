using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_150 : ICommand {

        public short ID { get; set; } = 19784;
        public bool close = false;

        public class_150(bool param1 = false) {
            this.close = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.close = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.close);
            param1.WriteShort(10305);
        }
    }
}
