using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HandshakeRequest : ICommand {

        public short ID { get; set; } = 31013;
        public byte[] var_4280;

        public HandshakeRequest(byte[] param1 = null) {
            this.var_4280 = new byte[0];
            if (param1 != null) {
                this.var_4280 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4280 = param1.ReadBytes();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBytes(this.var_4280);
        }
    }
}
