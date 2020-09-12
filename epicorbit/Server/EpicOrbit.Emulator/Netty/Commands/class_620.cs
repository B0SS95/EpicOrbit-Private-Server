using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_620 : ICommand {

        public short ID { get; set; } = 16453;
        public int mapId = 0;
        public int currentNpcOnMap = 0;
        public int maxNpcAmountForMap = 0;
        public int currentNpcKills = 0;
        public int killNpcId = 0;
        public int spawnNpcId = 0;

        public class_620(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0) {
            this.mapId = param1;
            this.killNpcId = param2;
            this.spawnNpcId = param3;
            this.currentNpcOnMap = param4;
            this.maxNpcAmountForMap = param5;
            this.currentNpcKills = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 17);
            this.currentNpcOnMap = param1.ReadInt();
            this.currentNpcOnMap = param1.Shift(this.currentNpcOnMap, 30);
            this.maxNpcAmountForMap = param1.ReadInt();
            this.maxNpcAmountForMap = param1.Shift(this.maxNpcAmountForMap, 25);
            this.currentNpcKills = param1.ReadInt();
            this.currentNpcKills = param1.Shift(this.currentNpcKills, 21);
            this.killNpcId = param1.ReadInt();
            this.killNpcId = param1.Shift(this.killNpcId, 15);
            this.spawnNpcId = param1.ReadInt();
            this.spawnNpcId = param1.Shift(this.spawnNpcId, 21);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.mapId, 15));
            param1.WriteInt(param1.Shift(this.currentNpcOnMap, 2));
            param1.WriteInt(param1.Shift(this.maxNpcAmountForMap, 7));
            param1.WriteInt(param1.Shift(this.currentNpcKills, 11));
            param1.WriteInt(param1.Shift(this.killNpcId, 17));
            param1.WriteInt(param1.Shift(this.spawnNpcId, 11));
            param1.WriteShort(-9798);
        }
    }
}
