using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_983 : ICommand {

        public short ID { get; set; } = 21397;
        public int id = 0;

        public class_983(int param1 = 0) {
            this.id = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 10);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.id, 22));
            param1.WriteShort(-4099);
        }
    }
}
