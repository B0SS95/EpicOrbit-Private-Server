using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_791 : ICommand {

        public short ID { get; set; } = 13865;
        public double var_4754 = 0;

        public class_791(double param1 = 0) {
            this.var_4754 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4754 = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_4754);
        }
    }
}
