using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_982 : ICommand {

        public short ID { get; set; } = 11783;
        public int var_2690 = 0;
        public int var_4311 = 0;

        public class_982(int param1 = 0, int param2 = 0) {
            this.var_2690 = param1;
            this.var_4311 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2690 = param1.ReadInt();
            this.var_2690 = param1.Shift(this.var_2690, 8);
            this.var_4311 = param1.ReadInt();
            this.var_4311 = param1.Shift(this.var_4311, 9);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(13769);
            param1.WriteInt(param1.Shift(this.var_2690, 24));
            param1.WriteInt(param1.Shift(this.var_4311, 23));
            param1.WriteShort(-4794);
        }
    }
}
