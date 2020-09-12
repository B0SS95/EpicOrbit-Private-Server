using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_837 : ICommand {

        public short ID { get; set; } = 11978;
        public int y = 0;
        public int x = 0;

        public class_837(int param1 = 0, int param2 = 0) {
            this.x = param1;
            this.y = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 15);
            param1.ReadShort();
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 17);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.y, 17));
            param1.WriteShort(27695);
            param1.WriteInt(param1.Shift(this.x, 15));
        }
    }
}
