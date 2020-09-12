using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_725 : class_607, ICommand {

        public override short ID { get; set; } = 7940;
        public int name_7 = 0;

        public class_725(int param1 = 0) {
            this.name_7 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.name_7 = param1.ReadInt();
            this.name_7 = param1.Shift(this.name_7, 20);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-14269);
            param1.WriteInt(param1.Shift(this.name_7, 12));
        }
    }
}
