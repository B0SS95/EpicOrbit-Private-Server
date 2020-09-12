using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_776 : ICommand {

        public short ID { get; set; } = 349;
        public int var_4638 = 0;

        public class_776(int param1 = 0) {
            this.var_4638 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.var_4638 = param1.ReadInt();
            this.var_4638 = param1.Shift(this.var_4638, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5768);
            param1.WriteShort(13314);
            param1.WriteInt(param1.Shift(this.var_4638, 24));
        }
    }
}
