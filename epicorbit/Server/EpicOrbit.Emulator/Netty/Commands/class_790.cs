using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_790 : ICommand {

        public short ID { get; set; } = 27321;
        public class_828 mode;

        public class_790(class_828 param1 = null) {
            if (param1 == null) {
                this.mode = new class_828();
            } else {
                this.mode = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mode = lookup.Lookup(param1) as class_828;
            this.mode.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.mode.Write(param1);
        }
    }
}
