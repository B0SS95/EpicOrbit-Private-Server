using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_555 : class_543, ICommand {

        public override short ID { get; set; } = 28967;
        public int var_4909 = 0;

        public class_555(int param1 = 0) {
            this.var_4909 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_4909 = param1.ReadInt();
            this.var_4909 = param1.Shift(this.var_4909, 30);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_4909, 2));
            param1.WriteShort(-14087);
        }
    }
}
