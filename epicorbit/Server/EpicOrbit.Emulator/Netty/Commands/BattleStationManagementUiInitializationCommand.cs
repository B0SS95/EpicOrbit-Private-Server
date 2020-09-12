using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BattleStationManagementUiInitializationCommand : ICommand {

        public short ID { get; set; } = 1279;
        public int deflectorShieldMinutesMax = 0;
        public int deflectorShieldMinutesIncrement = 0;
        public int deflectorShieldMinutesMin = 0;
        public int battleStationId = 0;
        public AvailableModulesCommand availableModules;
        public string battleStationName = "";
        public BattleStationStatusCommand state;
        public FactionModule faction;
        public bool deflectorDeactivationPossible = false;
        public string clanName = "";
        public int mapAssetId = 0;

        public BattleStationManagementUiInitializationCommand(int param1 = 0, int param2 = 0, string param3 = "", string param4 = "", FactionModule param5 = null, BattleStationStatusCommand param6 = null, AvailableModulesCommand param7 = null, int param8 = 0, int param9 = 0, int param10 = 0, bool param11 = false) {
            this.mapAssetId = param1;
            this.battleStationId = param2;
            this.battleStationName = param3;
            this.clanName = param4;
            if (param5 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param5;
            }
            if (param6 == null) {
                this.state = new BattleStationStatusCommand();
            } else {
                this.state = param6;
            }
            if (param7 == null) {
                this.availableModules = new AvailableModulesCommand();
            } else {
                this.availableModules = param7;
            }
            this.deflectorShieldMinutesMin = param8;
            this.deflectorShieldMinutesMax = param9;
            this.deflectorShieldMinutesIncrement = param10;
            this.deflectorDeactivationPossible = param11;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.deflectorShieldMinutesMax = param1.ReadInt();
            this.deflectorShieldMinutesMax = param1.Shift(this.deflectorShieldMinutesMax, 5);
            this.deflectorShieldMinutesIncrement = param1.ReadInt();
            this.deflectorShieldMinutesIncrement = param1.Shift(this.deflectorShieldMinutesIncrement, 2);
            this.deflectorShieldMinutesMin = param1.ReadInt();
            this.deflectorShieldMinutesMin = param1.Shift(this.deflectorShieldMinutesMin, 9);
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 21);
            this.availableModules = lookup.Lookup(param1) as AvailableModulesCommand;
            this.availableModules.Read(param1, lookup);
            this.battleStationName = param1.ReadUTF();
            this.state = lookup.Lookup(param1) as BattleStationStatusCommand;
            this.state.Read(param1, lookup);
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
            this.deflectorDeactivationPossible = param1.ReadBoolean();
            this.clanName = param1.ReadUTF();
            this.mapAssetId = param1.ReadInt();
            this.mapAssetId = param1.Shift(this.mapAssetId, 16);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.deflectorShieldMinutesMax, 27));
            param1.WriteInt(param1.Shift(this.deflectorShieldMinutesIncrement, 30));
            param1.WriteInt(param1.Shift(this.deflectorShieldMinutesMin, 23));
            param1.WriteInt(param1.Shift(this.battleStationId, 11));
            this.availableModules.Write(param1);
            param1.WriteUTF(this.battleStationName);
            this.state.Write(param1);
            this.faction.Write(param1);
            param1.WriteBoolean(this.deflectorDeactivationPossible);
            param1.WriteUTF(this.clanName);
            param1.WriteInt(param1.Shift(this.mapAssetId, 16));
        }
    }
}
