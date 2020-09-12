using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_793 : ICommand {

        public short ID { get; set; } = 7831;
        public int id = 0;

        public class_793(int param1 = 0) {
            this.id = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 4);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(15251);
            param1.WriteShort(32569);
            param1.WriteInt(param1.Shift(this.id, 28));
        }
    }
}
