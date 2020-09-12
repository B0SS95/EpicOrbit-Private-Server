using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_594 : ICommand {

        public const short const_2016 = 1;
        public const short const_1942 = 2;
        public const short NONE = 0;
        public short ID { get; set; } = 22245;
        public short reason = 0;
        public int var_2320 = 0;

        public class_594(int param1 = 0, short param2 = 0) {
            this.var_2320 = param1;
            this.reason = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.reason = param1.ReadShort();
            this.var_2320 = param1.ReadInt();
            this.var_2320 = param1.Shift(this.var_2320, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.reason);
            param1.WriteInt(param1.Shift(this.var_2320, 10));
        }
    }
}
