using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_506 : class_505, ICommand {

        public const short const_1596 = 0;
        public const short const_1935 = 2;
        public const short const_3185 = 1;
        public override short ID { get; set; } = 20711;
        public short type = 0;

        public class_506(short param1 = 0) {
            this.type = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.type = param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(this.type);
        }
    }
}
