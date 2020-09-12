using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_817 : ICommand {

        public short ID { get; set; } = 29456;
        public int var_1567 = 0;

        public class_817(int param1 = 0) {
            this.var_1567 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_1567 = param1.ReadInt();
            this.var_1567 = param1.Shift(this.var_1567, 18);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-224);
            param1.WriteInt(param1.Shift(this.var_1567, 14));
            param1.WriteShort(19562);
        }
    }
}
