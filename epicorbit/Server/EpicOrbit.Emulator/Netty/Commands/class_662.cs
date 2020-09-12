using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_662 : ICommand {

        public short ID { get; set; } = 5121;
        public int totalAmountOfNpcOnMap = 0;
        public int mapId = 0;
        public int npcId = 0;

        public class_662(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.mapId = param1;
            this.npcId = param2;
            this.totalAmountOfNpcOnMap = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.totalAmountOfNpcOnMap = param1.ReadInt();
            this.totalAmountOfNpcOnMap = param1.Shift(this.totalAmountOfNpcOnMap, 3);
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 31);
            this.npcId = param1.ReadInt();
            this.npcId = param1.Shift(this.npcId, 20);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.totalAmountOfNpcOnMap, 29));
            param1.WriteInt(param1.Shift(this.mapId, 1));
            param1.WriteInt(param1.Shift(this.npcId, 12));
        }
    }
}
