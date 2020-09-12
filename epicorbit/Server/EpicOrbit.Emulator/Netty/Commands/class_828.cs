using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_828 : ICommand {

        public const short const_163 = 0;
        public const short const_1207 = 1;
        public short ID { get; set; } = 29265;
        public short mode = 0;

        public class_828(short param1 = 0) {
            this.mode = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mode = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.mode);
            param1.WriteShort(23492);
        }
    }
}
