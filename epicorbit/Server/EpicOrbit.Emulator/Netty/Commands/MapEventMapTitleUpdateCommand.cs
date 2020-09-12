using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapEventMapTitleUpdateCommand : ICommand {

        public short ID { get; set; } = 4591;
        public string mapTitle = "";
        public int mapId = 0;

        public MapEventMapTitleUpdateCommand(int param1 = 0, string param2 = "") {
            this.mapId = param1;
            this.mapTitle = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.mapTitle = param1.ReadUTF();
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 5);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(32296);
            param1.WriteUTF(this.mapTitle);
            param1.WriteInt(param1.Shift(this.mapId, 27));
        }
    }
}
