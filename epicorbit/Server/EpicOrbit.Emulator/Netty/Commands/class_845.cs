using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_845 : ICommand {

        public short ID { get; set; } = 22685;
        public int size = 0;
        public byte[] code;

        public class_845(int param1 = 0, byte[] param2 = null) {
            this.code = new byte[0];
            this.size = param1;
            if (param2 != null) {
                this.code = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.size = param1.ReadInt();
            this.size = param1.Shift(this.size, 5);
            param1.ReadShort();
            this.code = param1.ReadBytes();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.size, 27));
            param1.WriteShort(-14943);
            param1.WriteBytes(this.code);
        }
    }
}
