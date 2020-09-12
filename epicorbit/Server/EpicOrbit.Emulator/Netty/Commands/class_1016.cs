using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1016 : ICommand {

        public short ID { get; set; } = 22558;
        public class_624 var_2932;

        public class_1016(class_624 param1 = null) {
            if (param1 == null) {
                this.var_2932 = new class_624();
            } else {
                this.var_2932 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2932 = lookup.Lookup(param1) as class_624;
            this.var_2932.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_2932.Write(param1);
        }
    }
}
