using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_550 : class_503, ICommand {

        public override short ID { get; set; } = 31584;
        public class_504 var_4436;
        public string name = "";
        public class_588 var_2874;

        public class_550(class_504 param1 = null, string param2 = "", class_588 param3 = null) {
            if (param1 == null) {
                this.var_4436 = new class_504();
            } else {
                this.var_4436 = param1;
            }
            this.name = param2;
            if (param3 == null) {
                this.var_2874 = new class_588();
            } else {
                this.var_2874 = param3;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_4436 = lookup.Lookup(param1) as class_504;
            this.var_4436.Read(param1, lookup);
            param1.ReadShort();
            this.name = param1.ReadUTF();
            this.var_2874 = lookup.Lookup(param1) as class_588;
            this.var_2874.Read(param1, lookup);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            this.var_4436.Write(param1);
            param1.WriteShort(32264);
            param1.WriteUTF(this.name);
            this.var_2874.Write(param1);
        }
    }
}
