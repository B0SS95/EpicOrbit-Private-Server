using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_800 : class_677, ICommand {

        public override short ID { get; set; } = 13069;
        public int amount = 0;

        public class_800(int param1 = 0, string param2 = "")
         : base(param2) {
            this.amount = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.amount = param1.ReadInt();
            this.amount = param1.Shift(this.amount, 24);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.amount, 8));
        }
    }
}
