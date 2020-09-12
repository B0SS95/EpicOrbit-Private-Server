using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UnequipModuleRequest : ICommand {

        public short ID { get; set; } = 31162;
        public int itemId = 0;
        public int battleStationId = 0;

        public UnequipModuleRequest(int param1 = 0, int param2 = 0) {
            this.battleStationId = param1;
            this.itemId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemId = param1.ReadInt();
            this.itemId = param1.Shift(this.itemId, 29);
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 29);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.itemId, 3));
            param1.WriteInt(param1.Shift(this.battleStationId, 3));
        }
    }
}
