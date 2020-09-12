using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class JumpCPUSelectCommand : ICommand {

        public short ID { get; set; } = 20422;
        public bool seletionAllowed = false;
        public int mapId = 0;
        public int price = 0;

        public JumpCPUSelectCommand(int param1 = 0, int param2 = 0, bool param3 = false) {
            this.mapId = param1;
            this.price = param2;
            this.seletionAllowed = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.seletionAllowed = param1.ReadBoolean();
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 11);
            param1.ReadShort();
            this.price = param1.ReadInt();
            this.price = param1.Shift(this.price, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.seletionAllowed);
            param1.WriteInt(param1.Shift(this.mapId, 21));
            param1.WriteShort(-2977);
            param1.WriteInt(param1.Shift(this.price, 10));
        }
    }
}
