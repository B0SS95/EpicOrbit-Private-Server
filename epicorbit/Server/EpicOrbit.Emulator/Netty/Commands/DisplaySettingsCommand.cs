using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DisplaySettingsCommand : ICommand {

        public short ID { get; set; } = 20579;
        public bool displayResources = false;
        public int name_42 = 0;
        public bool displayPlayerName = false;
        public int displaySetting3DqualityTextures = 0;
        public int name_13 = 0;
        public int displaySetting3DqualityLights = 0;
        public bool displayChat = false;
        public bool var_2596 = false;
        public bool var_1379 = false;
        public int displaySetting3DqualityEffects = 0;
        public bool showNotOwnedItems = false;
        public bool preloadUserShips = false;
        public bool var_1948 = false;
        public bool displayHitpointBubbles = false;
        public int displaySetting3DsizeTextures = 0;
        public bool displayBoxes = false;
        public bool proActionBarKeyboardInputEnabled = false;
        public bool displayDrones = false;
        public int displaySetting3DqualityAntialias = 0;
        public bool notSet = false;
        public bool var_3069 = false;
        public bool displayCargoboxes = false;
        public bool displayNotifications = false;
        public bool proActionBarEnabled = false;
        public bool var_1406 = false;
        public bool proActionBarAutohideEnabled = false;
        public bool var_3558 = false;
        public bool name_161 = false;
        public bool var_55 = false;
        public bool var_3236 = false;
        public bool displayPenaltyCargoboxes = false;
        public int displaySetting3DtextureFiltering = 0;

        public DisplaySettingsCommand(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false, bool param10 = false, bool param11 = false, bool param12 = false, bool param13 = false, bool param14 = false, bool param15 = false, bool param16 = false, bool param17 = false, bool param18 = false, bool param19 = false, int param20 = 0, int param21 = 0, int param22 = 0, int param23 = 0, int param24 = 0, int param25 = 0, int param26 = 0, int param27 = 0, bool param28 = false, bool param29 = false, bool param30 = false, bool param31 = false, bool param32 = false) {
            this.notSet = param1;
            this.displayPlayerName = param2;
            this.displayResources = param3;
            this.displayBoxes = param4;
            this.displayHitpointBubbles = param5;
            this.displayChat = param6;
            this.displayDrones = param7;
            this.displayCargoboxes = param8;
            this.displayPenaltyCargoboxes = param9;
            this.showNotOwnedItems = param10;
            this.var_3069 = param11;
            this.var_3236 = param12;
            this.displayNotifications = param13;
            this.preloadUserShips = param14;
            this.name_161 = param15;
            this.var_1406 = param16;
            this.var_1948 = param17;
            this.var_1379 = param18;
            this.var_55 = param19;
            this.displaySetting3DqualityAntialias = param20;
            this.name_42 = param21;
            this.displaySetting3DqualityEffects = param22;
            this.displaySetting3DqualityLights = param23;
            this.displaySetting3DqualityTextures = param24;
            this.name_13 = param25;
            this.displaySetting3DsizeTextures = param26;
            this.displaySetting3DtextureFiltering = param27;
            this.proActionBarEnabled = param28;
            this.proActionBarKeyboardInputEnabled = param29;
            this.proActionBarAutohideEnabled = param30;
            this.var_3558 = param31;
            this.var_2596 = param32;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.displayResources = param1.ReadBoolean();
            this.name_42 = param1.ReadInt();
            this.name_42 = param1.Shift(this.name_42, 29);
            this.displayPlayerName = param1.ReadBoolean();
            this.displaySetting3DqualityTextures = param1.ReadInt();
            this.displaySetting3DqualityTextures = param1.Shift(this.displaySetting3DqualityTextures, 20);
            this.name_13 = param1.ReadInt();
            this.name_13 = param1.Shift(this.name_13, 12);
            this.displaySetting3DqualityLights = param1.ReadInt();
            this.displaySetting3DqualityLights = param1.Shift(this.displaySetting3DqualityLights, 11);
            this.displayChat = param1.ReadBoolean();
            param1.ReadShort();
            this.var_2596 = param1.ReadBoolean();
            this.var_1379 = param1.ReadBoolean();
            this.displaySetting3DqualityEffects = param1.ReadInt();
            this.displaySetting3DqualityEffects = param1.Shift(this.displaySetting3DqualityEffects, 28);
            this.showNotOwnedItems = param1.ReadBoolean();
            param1.ReadShort();
            this.preloadUserShips = param1.ReadBoolean();
            this.var_1948 = param1.ReadBoolean();
            this.displayHitpointBubbles = param1.ReadBoolean();
            this.displaySetting3DsizeTextures = param1.ReadInt();
            this.displaySetting3DsizeTextures = param1.Shift(this.displaySetting3DsizeTextures, 23);
            this.displayBoxes = param1.ReadBoolean();
            this.proActionBarKeyboardInputEnabled = param1.ReadBoolean();
            this.displayDrones = param1.ReadBoolean();
            this.displaySetting3DqualityAntialias = param1.ReadInt();
            this.displaySetting3DqualityAntialias = param1.Shift(this.displaySetting3DqualityAntialias, 24);
            this.notSet = param1.ReadBoolean();
            this.var_3069 = param1.ReadBoolean();
            this.displayCargoboxes = param1.ReadBoolean();
            this.displayNotifications = param1.ReadBoolean();
            this.proActionBarEnabled = param1.ReadBoolean();
            this.var_1406 = param1.ReadBoolean();
            this.proActionBarAutohideEnabled = param1.ReadBoolean();
            this.var_3558 = param1.ReadBoolean();
            this.name_161 = param1.ReadBoolean();
            this.var_55 = param1.ReadBoolean();
            this.var_3236 = param1.ReadBoolean();
            this.displayPenaltyCargoboxes = param1.ReadBoolean();
            this.displaySetting3DtextureFiltering = param1.ReadInt();
            this.displaySetting3DtextureFiltering = param1.Shift(this.displaySetting3DtextureFiltering, 22);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.displayResources);
            param1.WriteInt(param1.Shift(this.name_42, 3));
            param1.WriteBoolean(this.displayPlayerName);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityTextures, 12));
            param1.WriteInt(param1.Shift(this.name_13, 20));
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityLights, 21));
            param1.WriteBoolean(this.displayChat);
            param1.WriteShort(3797);
            param1.WriteBoolean(this.var_2596);
            param1.WriteBoolean(this.var_1379);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityEffects, 4));
            param1.WriteBoolean(this.showNotOwnedItems);
            param1.WriteShort(-8095);
            param1.WriteBoolean(this.preloadUserShips);
            param1.WriteBoolean(this.var_1948);
            param1.WriteBoolean(this.displayHitpointBubbles);
            param1.WriteInt(param1.Shift(this.displaySetting3DsizeTextures, 9));
            param1.WriteBoolean(this.displayBoxes);
            param1.WriteBoolean(this.proActionBarKeyboardInputEnabled);
            param1.WriteBoolean(this.displayDrones);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityAntialias, 8));
            param1.WriteBoolean(this.notSet);
            param1.WriteBoolean(this.var_3069);
            param1.WriteBoolean(this.displayCargoboxes);
            param1.WriteBoolean(this.displayNotifications);
            param1.WriteBoolean(this.proActionBarEnabled);
            param1.WriteBoolean(this.var_1406);
            param1.WriteBoolean(this.proActionBarAutohideEnabled);
            param1.WriteBoolean(this.var_3558);
            param1.WriteBoolean(this.name_161);
            param1.WriteBoolean(this.var_55);
            param1.WriteBoolean(this.var_3236);
            param1.WriteBoolean(this.displayPenaltyCargoboxes);
            param1.WriteInt(param1.Shift(this.displaySetting3DtextureFiltering, 10));
        }
    }
}
