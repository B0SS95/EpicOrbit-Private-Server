using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class GameplaySettingsModule : ICommand {

        public short ID { get; set; } = 8750;
        public bool quickslotStopAttack = false;
        public bool doubleclickAttack = false;
        public bool autoChangeAmmo = false;
        public bool autoRefinement = false;
        public bool var_5281 = false;
        public bool autoBuyGreenBootyKeys = false;
        public bool showBattlerayNotifications = false;
        public bool notSet = false;
        public bool autoStart = false;
        public bool autoBoost = false;

        public GameplaySettingsModule(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false, bool param10 = false) {
            this.notSet = param1;
            this.autoBoost = param2;
            this.autoRefinement = param3;
            this.quickslotStopAttack = param4;
            this.doubleclickAttack = param5;
            this.autoChangeAmmo = param6;
            this.autoStart = param7;
            this.autoBuyGreenBootyKeys = param8;
            this.showBattlerayNotifications = param9;
            this.var_5281 = param10;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.quickslotStopAttack = param1.ReadBoolean();
            this.doubleclickAttack = param1.ReadBoolean();
            this.autoChangeAmmo = param1.ReadBoolean();
            this.autoRefinement = param1.ReadBoolean();
            this.var_5281 = param1.ReadBoolean();
            this.autoBuyGreenBootyKeys = param1.ReadBoolean();
            this.showBattlerayNotifications = param1.ReadBoolean();
            this.notSet = param1.ReadBoolean();
            this.autoStart = param1.ReadBoolean();
            this.autoBoost = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.quickslotStopAttack);
            param1.WriteBoolean(this.doubleclickAttack);
            param1.WriteBoolean(this.autoChangeAmmo);
            param1.WriteBoolean(this.autoRefinement);
            param1.WriteBoolean(this.var_5281);
            param1.WriteBoolean(this.autoBuyGreenBootyKeys);
            param1.WriteBoolean(this.showBattlerayNotifications);
            param1.WriteBoolean(this.notSet);
            param1.WriteBoolean(this.autoStart);
            param1.WriteBoolean(this.autoBoost);
        }
    }
}
