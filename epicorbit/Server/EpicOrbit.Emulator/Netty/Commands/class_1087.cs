using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1087 : ICommand {

        public const short const_2210 = 4;
        public const short REJECT = 3;
        public const short REVOKE = 2;
        public const short TIMEOUT = 1;
        public const short NONE = 0;
        public short ID { get; set; } = 1258;
        public int var_5329 = 0;
        public int var_4053 = 0;
        public short reason = 0;

        public class_1087(int param1 = 0, int param2 = 0, short param3 = 0) {
            this.var_4053 = param1;
            this.var_5329 = param2;
            this.reason = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5329 = param1.ReadInt();
            this.var_5329 = param1.Shift(this.var_5329, 19);
            this.var_4053 = param1.ReadInt();
            this.var_4053 = param1.Shift(this.var_4053, 2);
            this.reason = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_5329, 13));
            param1.WriteInt(param1.Shift(this.var_4053, 30));
            param1.WriteShort(this.reason);
        }
    }
}
