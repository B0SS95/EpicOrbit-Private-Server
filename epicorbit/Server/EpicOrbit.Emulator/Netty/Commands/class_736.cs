using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_736 : class_540, ICommand {

        public override short ID { get; set; } = 12951;
        public int value = 0;

        public class_736(string param1 = "", int param2 = 0)
         : base(param1) {
            this.value = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.value = param1.ReadInt();
            this.value = param1.Shift(this.value, 5);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.value, 27));
        }
    }
}
