using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_856 : class_607, ICommand {

        public override short ID { get; set; } = 5547;
        public int var_1962 = 0;

        public class_856(int param1 = 0) {
            this.var_1962 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_1962 = param1.ReadInt();
            this.var_1962 = param1.Shift(this.var_1962, 18);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(24470);
            param1.WriteInt(param1.Shift(this.var_1962, 14));
        }
    }
}
