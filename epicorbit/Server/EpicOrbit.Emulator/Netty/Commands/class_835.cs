using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_835 : ICommand {

        public short ID { get; set; } = 18010;
        public double var_5241 = 0;
        public double var_5146 = 0;

        public class_835(double param1 = 0, double param2 = 0) {
            this.var_5241 = param1;
            this.var_5146 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5241 = param1.ReadDouble();
            this.var_5146 = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_5241);
            param1.WriteDouble(this.var_5146);
        }
    }
}
