using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1092 : ICommand {

        public short ID { get; set; } = 9068;
        public int var_3015 = 0;

        public class_1092(int param1 = 0) {
            this.var_3015 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.var_3015 = param1.ReadInt();
            this.var_3015 = param1.Shift(this.var_3015, 27);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-7259);
            param1.WriteShort(21538);
            param1.WriteInt(param1.Shift(this.var_3015, 5));
        }
    }
}
