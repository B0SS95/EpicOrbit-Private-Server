using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_886 : ICommand {

        public short ID { get; set; } = 24913;
        public double var_3450 = 0;

        public class_886(double param1 = 0) {
            this.var_3450 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3450 = param1.ReadDouble();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_3450);
            param1.WriteShort(21133);
            param1.WriteShort(4399);
        }
    }
}
