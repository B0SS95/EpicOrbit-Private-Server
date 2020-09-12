using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_642 : ICommand {

        public short ID { get; set; } = 13764;
        public int var_2222 = 0;

        public class_642(int param1 = 0) {
            this.var_2222 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2222 = param1.ReadInt();
            this.var_2222 = param1.Shift(this.var_2222, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2222, 13));
        }
    }
}
