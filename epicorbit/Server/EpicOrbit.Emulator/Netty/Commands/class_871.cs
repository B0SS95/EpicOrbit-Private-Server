using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_871 : class_607, ICommand {

        public override short ID { get; set; } = 13106;
        public int timer = 0;

        public class_871(int param1 = 0) {
            this.timer = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.timer = param1.ReadInt();
            this.timer = param1.Shift(this.timer, 6);
            param1.ReadShort();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.timer, 26));
            param1.WriteShort(13479);
            param1.WriteShort(-21386);
        }
    }
}
