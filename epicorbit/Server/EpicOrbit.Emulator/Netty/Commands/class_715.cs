using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_715 : class_505, ICommand {

        public override short ID { get; set; } = 13981;
        public class_1021 var_4674;
        public int timer = 0;

        public class_715(class_1021 param1 = null, int param2 = 0) {
            this.timer = param2;
            if (param1 == null) {
                this.var_4674 = new class_1021();
            } else {
                this.var_4674 = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_4674 = lookup.Lookup(param1) as class_1021;
            this.var_4674.Read(param1, lookup);
            this.timer = param1.ReadInt();
            this.timer = param1.Shift(this.timer, 28);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(10532);
            this.var_4674.Write(param1);
            param1.WriteInt(param1.Shift(this.timer, 4));
        }
    }
}
