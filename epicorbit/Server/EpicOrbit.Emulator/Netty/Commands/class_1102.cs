using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1102 : class_748, ICommand {

        public override short ID { get; set; } = 13269;
        public int max = 0;
        public int min = 0;

        public class_1102(int param1 = 0, int param2 = 0) {
            this.min = param1;
            this.max = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.max = param1.ReadInt();
            this.max = param1.Shift(this.max, 16);
            this.min = param1.ReadInt();
            this.min = param1.Shift(this.min, 20);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.max, 16));
            param1.WriteInt(param1.Shift(this.min, 12));
        }
    }
}
