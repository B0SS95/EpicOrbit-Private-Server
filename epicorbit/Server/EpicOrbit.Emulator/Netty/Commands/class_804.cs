using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_804 : ICommand {

        public short ID { get; set; } = 17289;
        public int mapId = 0;

        public class_804(int param1 = 0) {
            this.mapId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 12);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.mapId, 20));
            param1.WriteShort(5099);
            param1.WriteShort(-28092);
        }
    }
}
