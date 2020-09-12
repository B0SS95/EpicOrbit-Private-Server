using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BattleStationBuildingStateCommand : ICommand {

        public short ID { get; set; } = 11738;
        public string ownerClan = "";
        public string battleStationName = "";
        public int battleStationId = 0;
        public FactionModule affiliatedFaction;
        public int mapAssetId = 0;
        public int totalSeconds = 0;
        public int secondsLeft = 0;

        public BattleStationBuildingStateCommand(int param1 = 0, int param2 = 0, string param3 = "", int param4 = 0, int param5 = 0, string param6 = "", FactionModule param7 = null) {
            this.mapAssetId = param1;
            this.battleStationId = param2;
            this.battleStationName = param3;
            this.secondsLeft = param4;
            this.totalSeconds = param5;
            this.ownerClan = param6;
            if (param7 == null) {
                this.affiliatedFaction = new FactionModule();
            } else {
                this.affiliatedFaction = param7;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ownerClan = param1.ReadUTF();
            this.battleStationName = param1.ReadUTF();
            param1.ReadShort();
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 30);
            this.affiliatedFaction = lookup.Lookup(param1) as FactionModule;
            this.affiliatedFaction.Read(param1, lookup);
            this.mapAssetId = param1.ReadInt();
            this.mapAssetId = param1.Shift(this.mapAssetId, 7);
            this.totalSeconds = param1.ReadInt();
            this.totalSeconds = param1.Shift(this.totalSeconds, 20);
            param1.ReadShort();
            this.secondsLeft = param1.ReadInt();
            this.secondsLeft = param1.Shift(this.secondsLeft, 25);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.ownerClan);
            param1.WriteUTF(this.battleStationName);
            param1.WriteShort(-21895);
            param1.WriteInt(param1.Shift(this.battleStationId, 2));
            this.affiliatedFaction.Write(param1);
            param1.WriteInt(param1.Shift(this.mapAssetId, 25));
            param1.WriteInt(param1.Shift(this.totalSeconds, 12));
            param1.WriteShort(2976);
            param1.WriteInt(param1.Shift(this.secondsLeft, 7));
        }
    }
}
