using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_687 : ICommand {

        public const short SCREEN = 1;
        public const short LOG = 0;
        public const short BOTH = 2;
        public const short NORMAL = 1;
        public const short const_1456 = 0;
        public short ID { get; set; } = 12338;
        public short var_3599 = 0;
        public class_635 var_210;
        public short var_4722 = 0;

        public class_687(short param1 = 0, short param2 = 0, class_635 param3 = null) {
            this.var_3599 = param1;
            this.var_4722 = param2;
            if (param3 == null) {
                this.var_210 = new class_635();
            } else {
                this.var_210 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3599 = param1.ReadShort();
            this.var_210 = lookup.Lookup(param1) as class_635;
            this.var_210.Read(param1, lookup);
            this.var_4722 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_3599);
            this.var_210.Write(param1);
            param1.WriteShort(this.var_4722);
        }
    }
}
