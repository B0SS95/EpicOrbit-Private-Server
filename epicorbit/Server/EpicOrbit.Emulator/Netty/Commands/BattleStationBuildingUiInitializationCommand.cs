using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BattleStationBuildingUiInitializationCommand : ICommand {

        public short ID { get; set; } = 13862;
        public int buildTimeInMinutesMin = 0;
        public int battleStationId = 0;
        public int buildTimeInMinutesMax = 0;
        public AvailableModulesCommand availableModules;
        public string battleStationName = "";
        public int mapAssetId = 0;
        public int buildTimeInMinutesIncrement = 0;
        public AsteroidProgressCommand progress;

        public BattleStationBuildingUiInitializationCommand(int param1 = 0, int param2 = 0, string param3 = "", AsteroidProgressCommand param4 = null, AvailableModulesCommand param5 = null, int param6 = 0, int param7 = 0, int param8 = 0) {
            this.mapAssetId = param1;
            this.battleStationId = param2;
            this.battleStationName = param3;
            if (param4 == null) {
                this.progress = new AsteroidProgressCommand();
            } else {
                this.progress = param4;
            }
            if (param5 == null) {
                this.availableModules = new AvailableModulesCommand();
            } else {
                this.availableModules = param5;
            }
            this.buildTimeInMinutesMin = param6;
            this.buildTimeInMinutesMax = param7;
            this.buildTimeInMinutesIncrement = param8;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.buildTimeInMinutesMin = param1.ReadInt();
            this.buildTimeInMinutesMin = param1.Shift(this.buildTimeInMinutesMin, 19);
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 8);
            this.buildTimeInMinutesMax = param1.ReadInt();
            this.buildTimeInMinutesMax = param1.Shift(this.buildTimeInMinutesMax, 17);
            this.availableModules = lookup.Lookup(param1) as AvailableModulesCommand;
            this.availableModules.Read(param1, lookup);
            this.battleStationName = param1.ReadUTF();
            this.mapAssetId = param1.ReadInt();
            this.mapAssetId = param1.Shift(this.mapAssetId, 23);
            this.buildTimeInMinutesIncrement = param1.ReadInt();
            this.buildTimeInMinutesIncrement = param1.Shift(this.buildTimeInMinutesIncrement, 22);
            this.progress = lookup.Lookup(param1) as AsteroidProgressCommand;
            this.progress.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.buildTimeInMinutesMin, 13));
            param1.WriteInt(param1.Shift(this.battleStationId, 24));
            param1.WriteInt(param1.Shift(this.buildTimeInMinutesMax, 15));
            this.availableModules.Write(param1);
            param1.WriteUTF(this.battleStationName);
            param1.WriteInt(param1.Shift(this.mapAssetId, 9));
            param1.WriteInt(param1.Shift(this.buildTimeInMinutesIncrement, 10));
            this.progress.Write(param1);
            param1.WriteShort(-15419);
        }
    }
}
