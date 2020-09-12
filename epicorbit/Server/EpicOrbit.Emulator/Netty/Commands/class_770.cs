using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_770 : ICommand {

        public short ID { get; set; } = 1968;
        public int var_2243 = 0;

        public class_770(int param1 = 0) {
            this.var_2243 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2243 = param1.ReadInt();
            this.var_2243 = param1.Shift(this.var_2243, 17);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2243, 15));
            param1.WriteShort(8170);
        }
    }
}
