using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_604 : ICommand {

        public short ID { get; set; } = 23885;
        public int uid = 0;
        public int var_45 = 0;

        public class_604(int param1 = 0, int param2 = 0) {
            this.uid = param1;
            this.var_45 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 8);
            param1.ReadShort();
            this.var_45 = param1.ReadInt();
            this.var_45 = param1.Shift(this.var_45, 31);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(6750);
            param1.WriteInt(param1.Shift(this.uid, 24));
            param1.WriteShort(-11300);
            param1.WriteInt(param1.Shift(this.var_45, 1));
        }
    }
}
