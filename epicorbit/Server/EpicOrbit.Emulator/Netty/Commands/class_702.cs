using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_702 : ICommand {

        public const short const_3316 = 7;
        public const short const_594 = 4;
        public const short REGISTRATION = 0;
        public const short const_1152 = 6;
        public const short const_3347 = 2;
        public const short CONFIRMATION = 3;
        public const short const_1471 = 5;
        public const short REGISTRATION_UNAVAILABLE = 1;
        public short ID { get; set; } = 5503;
        public short var_2563 = 0;
        public class_505 var_1355;

        public class_702(class_505 param1 = null, short param2 = 0) {
            if (param1 == null) {
                this.var_1355 = new class_505();
            } else {
                this.var_1355 = param1;
            }
            this.var_2563 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2563 = param1.ReadShort();
            this.var_1355 = lookup.Lookup(param1) as class_505;
            this.var_1355.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_2563);
            this.var_1355.Write(param1);
        }
    }
}
