using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_998 : ICommand {

        public short ID { get; set; } = 21592;
        public bool visible = false;
        public int shipId = 0;

        public class_998(int param1 = 0, bool param2 = false) {
            this.shipId = param1;
            this.visible = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.visible = param1.ReadBoolean();
            this.shipId = param1.ReadInt();
            this.shipId = param1.Shift(this.shipId, 9);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.visible);
            param1.WriteInt(param1.Shift(this.shipId, 23));
        }
    }
}
