using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SettingsOldFlashCommand : ICommand {

        public short ID { get; set; } = 13976;
        public bool simpleOpponents = false;
        public bool showStarsystem = false;
        public AmmunitionTypeModule var_5126;
        public bool displayPlayerName = false;
        public bool displayExplosions = false;
        public bool displayDamage = false;
        public bool sound = false;
        public bool displayHitpointsBubbles = false;
        public bool ignoreResources = false;
        public bool displayChat = false;
        public bool displayAllLasers = false;
        public bool music = false;
        public bool ignoreHostileCARGO = false;
        public bool displayFractionIcon = false;
        public AmmunitionTypeModule var_1504;
        public bool ignoreCARGO = false;
        public bool displayMessages = false;
        public bool autoBoost = false;
        public bool ignoreCargoBoxes = false;
        public bool enableFastBuy = false;
        public bool displayDrones = false;
        public bool simpleGates = false;
        public bool displayDigits = false;
        public bool autochangeAmmo = false;
        public bool displayAlphaBG = false;

        public SettingsOldFlashCommand(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false, bool param10 = false, bool param11 = false, bool param12 = false, bool param13 = false, bool param14 = false, bool param15 = false, bool param16 = false, bool param17 = false, bool param18 = false, bool param19 = false, bool param20 = false, bool param21 = false, bool param22 = false, bool param23 = false, AmmunitionTypeModule param24 = null, AmmunitionTypeModule param25 = null) {
            this.autoBoost = param1;
            this.displayDamage = param2;
            this.displayAllLasers = param3;
            this.displayExplosions = param4;
            this.displayPlayerName = param5;
            this.displayFractionIcon = param6;
            this.displayAlphaBG = param7;
            this.ignoreResources = param8;
            this.ignoreCargoBoxes = param9;
            this.simpleGates = param10;
            this.simpleOpponents = param11;
            this.sound = param12;
            this.music = param13;
            this.displayMessages = param14;
            this.displayHitpointsBubbles = param15;
            this.displayDigits = param16;
            this.displayChat = param17;
            this.displayDrones = param18;
            this.showStarsystem = param19;
            this.ignoreCARGO = param20;
            this.ignoreHostileCARGO = param21;
            this.autochangeAmmo = param22;
            this.enableFastBuy = param23;
            if (param24 == null) {
                this.var_5126 = new AmmunitionTypeModule();
            } else {
                this.var_5126 = param24;
            }
            if (param25 == null) {
                this.var_1504 = new AmmunitionTypeModule();
            } else {
                this.var_1504 = param25;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.simpleOpponents = param1.ReadBoolean();
            this.showStarsystem = param1.ReadBoolean();
            this.var_5126 = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.var_5126.Read(param1, lookup);
            this.displayPlayerName = param1.ReadBoolean();
            this.displayExplosions = param1.ReadBoolean();
            this.displayDamage = param1.ReadBoolean();
            this.sound = param1.ReadBoolean();
            this.displayHitpointsBubbles = param1.ReadBoolean();
            this.ignoreResources = param1.ReadBoolean();
            this.displayChat = param1.ReadBoolean();
            this.displayAllLasers = param1.ReadBoolean();
            this.music = param1.ReadBoolean();
            this.ignoreHostileCARGO = param1.ReadBoolean();
            this.displayFractionIcon = param1.ReadBoolean();
            this.var_1504 = lookup.Lookup(param1) as AmmunitionTypeModule;
            this.var_1504.Read(param1, lookup);
            this.ignoreCARGO = param1.ReadBoolean();
            this.displayMessages = param1.ReadBoolean();
            this.autoBoost = param1.ReadBoolean();
            this.ignoreCargoBoxes = param1.ReadBoolean();
            this.enableFastBuy = param1.ReadBoolean();
            this.displayDrones = param1.ReadBoolean();
            this.simpleGates = param1.ReadBoolean();
            this.displayDigits = param1.ReadBoolean();
            this.autochangeAmmo = param1.ReadBoolean();
            this.displayAlphaBG = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.simpleOpponents);
            param1.WriteBoolean(this.showStarsystem);
            this.var_5126.Write(param1);
            param1.WriteBoolean(this.displayPlayerName);
            param1.WriteBoolean(this.displayExplosions);
            param1.WriteBoolean(this.displayDamage);
            param1.WriteBoolean(this.sound);
            param1.WriteBoolean(this.displayHitpointsBubbles);
            param1.WriteBoolean(this.ignoreResources);
            param1.WriteBoolean(this.displayChat);
            param1.WriteBoolean(this.displayAllLasers);
            param1.WriteBoolean(this.music);
            param1.WriteBoolean(this.ignoreHostileCARGO);
            param1.WriteBoolean(this.displayFractionIcon);
            this.var_1504.Write(param1);
            param1.WriteBoolean(this.ignoreCARGO);
            param1.WriteBoolean(this.displayMessages);
            param1.WriteBoolean(this.autoBoost);
            param1.WriteBoolean(this.ignoreCargoBoxes);
            param1.WriteBoolean(this.enableFastBuy);
            param1.WriteBoolean(this.displayDrones);
            param1.WriteBoolean(this.simpleGates);
            param1.WriteBoolean(this.displayDigits);
            param1.WriteBoolean(this.autochangeAmmo);
            param1.WriteBoolean(this.displayAlphaBG);
        }
    }
}
