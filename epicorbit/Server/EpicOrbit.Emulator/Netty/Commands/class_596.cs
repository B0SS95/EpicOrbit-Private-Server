using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_596 : ICommand {

        public short ID { get; set; } = 29320;
        public int var_4330 = 0;

        public class_596(int param1 = 0) {
            this.var_4330 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4330 = param1.ReadInt();
            this.var_4330 = param1.Shift(this.var_4330, 8);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_4330, 24));
            param1.WriteShort(-29221);
            param1.WriteShort(-27358);
        }
    }
}
