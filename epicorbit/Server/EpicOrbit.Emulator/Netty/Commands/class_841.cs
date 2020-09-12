using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_841 : ICommand {

        public short ID { get; set; } = 7008;
        public int var_983 = 0;

        public class_841(int param1 = 0) {
            this.var_983 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_983 = param1.ReadInt();
            this.var_983 = param1.Shift(this.var_983, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-417);
            param1.WriteInt(param1.Shift(this.var_983, 18));
        }
    }
}
