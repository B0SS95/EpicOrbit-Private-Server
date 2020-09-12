using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_591 : class_503, ICommand {

        public override short ID { get; set; } = 10608;
        public int level = 0;

        public class_591(int param1 = 0) {
            this.level = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 5);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.level, 27));
            param1.WriteShort(-2498);
        }
    }
}
