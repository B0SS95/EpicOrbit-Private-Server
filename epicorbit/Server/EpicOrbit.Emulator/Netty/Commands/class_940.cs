using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_940 : class_796, ICommand {

        public override short ID { get; set; } = 12192;
        public double var_3461 = 0;

        public class_940(double param1 = 0) {
            this.var_3461 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_3461 = param1.ReadDouble();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(12696);
            param1.WriteDouble(this.var_3461);
        }
    }
}
