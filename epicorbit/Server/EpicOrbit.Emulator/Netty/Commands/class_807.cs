using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_807 : ICommand {

        public short ID { get; set; } = 1537;
        public byte[] var_3596;

        public class_807(byte[] param1 = null) {
            this.var_3596 = new byte[0];
            if (param1 != null) {
                this.var_3596 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.var_3596 = param1.ReadBytes();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(866);
            param1.WriteShort(-4575);
            param1.WriteBytes(this.var_3596);
        }
    }
}
