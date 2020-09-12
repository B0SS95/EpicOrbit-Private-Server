using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_501 : ICommand {

        public const short const_964 = 7;
        public const short const_651 = 9;
        public const short const_114 = 0;
        public const short const_2452 = 8;
        public const short const_163 = 5;
        public const short const_1962 = 2;
        public const short const_1309 = 1;
        public const short const_1451 = 3;
        public const short const_555 = 4;
        public const short const_2267 = 6;
        public short ID { get; set; } = 229;
        public int var_5329 = 0;
        public int var_4053 = 0;
        public short error = 0;

        public class_501(int param1 = 0, int param2 = 0, short param3 = 0) {
            this.var_4053 = param1;
            this.var_5329 = param2;
            this.error = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5329 = param1.ReadInt();
            this.var_5329 = param1.Shift(this.var_5329, 18);
            this.var_4053 = param1.ReadInt();
            this.var_4053 = param1.Shift(this.var_4053, 20);
            this.error = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_5329, 14));
            param1.WriteInt(param1.Shift(this.var_4053, 12));
            param1.WriteShort(this.error);
        }
    }
}
