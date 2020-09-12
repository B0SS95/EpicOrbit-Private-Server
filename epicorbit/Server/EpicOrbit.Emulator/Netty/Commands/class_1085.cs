using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1085 : ICommand {

        public const short const_809 = 0;
        public const short const_1775 = 1;
        public short ID { get; set; } = 13571;
        public int var_274 = 0;
        public short var_2563 = 0;

        public class_1085(int param1 = 0, short param2 = 0) {
            this.var_274 = param1;
            this.var_2563 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_274 = param1.ReadInt();
            this.var_274 = param1.Shift(this.var_274, 25);
            this.var_2563 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_274, 7));
            param1.WriteShort(this.var_2563);
        }
    }
}
