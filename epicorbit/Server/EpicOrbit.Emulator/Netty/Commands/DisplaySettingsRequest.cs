using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DisplaySettingsRequest : ICommand {

        public short ID { get; set; } = 24784;
        public bool proActionBarKeyboardInputEnabled = false;
        public bool proActionBarEnabled = false;
        public bool var_1379 = false;
        public int displaySetting3DtextureFiltering = 0;
        public int name_42 = 0;
        public bool displayHitpointBubbles = false;
        public bool displayCargoboxes = false;
        public bool var_3069 = false;
        public bool var_55 = false;
        public bool displayResources = false;
        public bool displayBoxes = false;
        public bool preloadUserShips = false;
        public int displaySetting3DsizeTextures = 0;
        public bool displayPlayerName = false;
        public bool proActionBarAutohideEnabled = false;
        public bool displayPenaltyCargoboxes = false;
        public bool name_161 = false;
        public bool var_2596 = false;
        public bool displayNotifications = false;
        public bool showNotOwnedItems = false;
        public bool var_1948 = false;
        public int displaySetting3DqualityLights = 0;
        public bool displayChat = false;
        public int name_13 = 0;
        public int displaySetting3DqualityEffects = 0;
        public bool var_3236 = false;
        public bool displayDrones = false;
        public int displaySetting3DqualityTextures = 0;
        public bool var_3558 = false;
        public int displaySetting3DqualityAntialias = 0;
        public bool var_1406 = false;

        public DisplaySettingsRequest(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false, bool param10 = false, bool param11 = false, bool param12 = false, bool param13 = false, bool param14 = false, bool param15 = false, bool param16 = false, bool param17 = false, bool param18 = false, int param19 = 0, int param20 = 0, int param21 = 0, int param22 = 0, int param23 = 0, int param24 = 0, int param25 = 0, int param26 = 0, bool param27 = false, bool param28 = false, bool param29 = false, bool param30 = false, bool param31 = false) {
            this.displayPlayerName = param1;
            this.displayResources = param2;
            this.displayBoxes = param3;
            this.displayHitpointBubbles = param4;
            this.displayChat = param5;
            this.displayDrones = param6;
            this.displayCargoboxes = param7;
            this.displayPenaltyCargoboxes = param8;
            this.showNotOwnedItems = param9;
            this.var_3069 = param10;
            this.var_3236 = param11;
            this.displayNotifications = param12;
            this.preloadUserShips = param13;
            this.name_161 = param14;
            this.var_1406 = param15;
            this.var_1948 = param16;
            this.var_1379 = param17;
            this.var_55 = param18;
            this.displaySetting3DqualityAntialias = param19;
            this.name_42 = param20;
            this.displaySetting3DqualityEffects = param21;
            this.displaySetting3DqualityLights = param22;
            this.displaySetting3DqualityTextures = param23;
            this.name_13 = param24;
            this.displaySetting3DsizeTextures = param25;
            this.displaySetting3DtextureFiltering = param26;
            this.proActionBarEnabled = param27;
            this.proActionBarKeyboardInputEnabled = param28;
            this.proActionBarAutohideEnabled = param29;
            this.var_3558 = param30;
            this.var_2596 = param31;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.proActionBarKeyboardInputEnabled = param1.ReadBoolean();
            this.proActionBarEnabled = param1.ReadBoolean();
            this.var_1379 = param1.ReadBoolean();
            this.displaySetting3DtextureFiltering = param1.ReadInt();
            this.displaySetting3DtextureFiltering = param1.Shift(this.displaySetting3DtextureFiltering, 2);
            this.name_42 = param1.ReadInt();
            this.name_42 = param1.Shift(this.name_42, 31);
            this.displayHitpointBubbles = param1.ReadBoolean();
            this.displayCargoboxes = param1.ReadBoolean();
            this.var_3069 = param1.ReadBoolean();
            this.var_55 = param1.ReadBoolean();
            this.displayResources = param1.ReadBoolean();
            this.displayBoxes = param1.ReadBoolean();
            this.preloadUserShips = param1.ReadBoolean();
            this.displaySetting3DsizeTextures = param1.ReadInt();
            this.displaySetting3DsizeTextures = param1.Shift(this.displaySetting3DsizeTextures, 7);
            this.displayPlayerName = param1.ReadBoolean();
            this.proActionBarAutohideEnabled = param1.ReadBoolean();
            this.displayPenaltyCargoboxes = param1.ReadBoolean();
            this.name_161 = param1.ReadBoolean();
            this.var_2596 = param1.ReadBoolean();
            this.displayNotifications = param1.ReadBoolean();
            this.showNotOwnedItems = param1.ReadBoolean();
            this.var_1948 = param1.ReadBoolean();
            this.displaySetting3DqualityLights = param1.ReadInt();
            this.displaySetting3DqualityLights = param1.Shift(this.displaySetting3DqualityLights, 12);
            this.displayChat = param1.ReadBoolean();
            this.name_13 = param1.ReadInt();
            this.name_13 = param1.Shift(this.name_13, 26);
            this.displaySetting3DqualityEffects = param1.ReadInt();
            this.displaySetting3DqualityEffects = param1.Shift(this.displaySetting3DqualityEffects, 13);
            param1.ReadShort();
            this.var_3236 = param1.ReadBoolean();
            this.displayDrones = param1.ReadBoolean();
            this.displaySetting3DqualityTextures = param1.ReadInt();
            this.displaySetting3DqualityTextures = param1.Shift(this.displaySetting3DqualityTextures, 30);
            this.var_3558 = param1.ReadBoolean();
            this.displaySetting3DqualityAntialias = param1.ReadInt();
            this.displaySetting3DqualityAntialias = param1.Shift(this.displaySetting3DqualityAntialias, 8);
            this.var_1406 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.proActionBarKeyboardInputEnabled);
            param1.WriteBoolean(this.proActionBarEnabled);
            param1.WriteBoolean(this.var_1379);
            param1.WriteInt(param1.Shift(this.displaySetting3DtextureFiltering, 30));
            param1.WriteInt(param1.Shift(this.name_42, 1));
            param1.WriteBoolean(this.displayHitpointBubbles);
            param1.WriteBoolean(this.displayCargoboxes);
            param1.WriteBoolean(this.var_3069);
            param1.WriteBoolean(this.var_55);
            param1.WriteBoolean(this.displayResources);
            param1.WriteBoolean(this.displayBoxes);
            param1.WriteBoolean(this.preloadUserShips);
            param1.WriteInt(param1.Shift(this.displaySetting3DsizeTextures, 25));
            param1.WriteBoolean(this.displayPlayerName);
            param1.WriteBoolean(this.proActionBarAutohideEnabled);
            param1.WriteBoolean(this.displayPenaltyCargoboxes);
            param1.WriteBoolean(this.name_161);
            param1.WriteBoolean(this.var_2596);
            param1.WriteBoolean(this.displayNotifications);
            param1.WriteBoolean(this.showNotOwnedItems);
            param1.WriteBoolean(this.var_1948);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityLights, 20));
            param1.WriteBoolean(this.displayChat);
            param1.WriteInt(param1.Shift(this.name_13, 6));
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityEffects, 19));
            param1.WriteShort(-3927);
            param1.WriteBoolean(this.var_3236);
            param1.WriteBoolean(this.displayDrones);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityTextures, 2));
            param1.WriteBoolean(this.var_3558);
            param1.WriteInt(param1.Shift(this.displaySetting3DqualityAntialias, 24));
            param1.WriteBoolean(this.var_1406);
        }
    }
}
