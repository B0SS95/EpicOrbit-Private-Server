using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_756 : class_748, ICommand {

        public override short ID { get; set; } = 19522;
        public class_788 var_15;
        public class_788 var_315;

        public class_756(class_788 param1 = null, class_788 param2 = null) {
            if (param2 == null) {
                this.var_15 = new class_788();
            } else {
                this.var_15 = param2;
            }
            if (param1 == null) {
                this.var_315 = new class_788();
            } else {
                this.var_315 = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_15 = lookup.Lookup(param1) as class_788;
            this.var_15.Read(param1, lookup);
            this.var_315 = lookup.Lookup(param1) as class_788;
            this.var_315.Read(param1, lookup);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            this.var_15.Write(param1);
            this.var_315.Write(param1);
            param1.WriteShort(25094);
        }
    }
}
