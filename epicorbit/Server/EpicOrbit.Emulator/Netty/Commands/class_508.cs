using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_508 : class_507, ICommand {

        public const short const_143 = 1;
        public const short const_515 = 5;
        public const short const_1875 = 0;
        public const short const_1471 = 2;
        public const short const_1026 = 4;
        public const short const_661 = 3;
        public const short const_3471 = 6;
        public override short ID { get; set; } = 21069;
        public short type = 0;

        public class_508(short param1 = 0) {
            this.type = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.type = param1.ReadShort();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-24347);
            param1.WriteShort(this.type);
            param1.WriteShort(27185);
        }
    }
}
