using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_683 : ICommand {

        public short ID { get; set; } = 696;
        public int var_1876 = 0;

        public class_683(int param1 = 0) {
            this.var_1876 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1876 = param1.ReadInt();
            this.var_1876 = param1.Shift(this.var_1876, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1876, 13));
        }
    }
}
