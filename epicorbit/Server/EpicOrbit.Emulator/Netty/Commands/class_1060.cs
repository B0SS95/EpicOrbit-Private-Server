using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1060 : ICommand {

        public short ID { get; set; } = 24574;
        public class_578 var_1305;
        public class_563 var_5170;

        public class_1060(class_578 param1 = null, class_563 param2 = null) {
            if (param1 == null) {
                this.var_1305 = new class_578();
            } else {
                this.var_1305 = param1;
            }
            if (param2 == null) {
                this.var_5170 = new class_563();
            } else {
                this.var_5170 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1305 = lookup.Lookup(param1) as class_578;
            this.var_1305.Read(param1, lookup);
            this.var_5170 = lookup.Lookup(param1) as class_563;
            this.var_5170.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_1305.Write(param1);
            this.var_5170.Write(param1);
            param1.WriteShort(-19160);
        }
    }
}
