using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_514 : class_507, ICommand {

        public const short const_2345 = 3;
        public const short const_1811 = 2;
        public const short const_96 = 0;
        public const short const_2397 = 1;
        public override short ID { get; set; } = 18640;
        public int number = 0;
        public short type = 0;

        public class_514(int param1 = 0, short param2 = 0) {
            this.type = param2;
            this.number = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.number = param1.ReadInt();
            this.number = param1.Shift(this.number, 15);
            this.type = param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.number, 17));
            param1.WriteShort(this.type);
        }
    }
}
