using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1047 : ICommand {

        public short ID { get; set; } = 21667;
        public int name_99 = 0;

        public class_1047(int param1 = 0) {
            this.name_99 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.name_99 = param1.ReadInt();
            this.name_99 = param1.Shift(this.name_99, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(6154);
            param1.WriteShort(22605);
            param1.WriteInt(param1.Shift(this.name_99, 24));
        }
    }
}
