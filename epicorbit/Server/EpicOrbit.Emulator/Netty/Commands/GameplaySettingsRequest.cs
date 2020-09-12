using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class GameplaySettingsRequest : ICommand {

        public short ID { get; set; } = 16691;
        public bool var_5281 = false;
        public bool quickslotStopAttack = false;
        public bool doubleclickAttack = false;
        public bool autoBuyGreenBootyKeys = false;
        public bool autoStart = false;
        public bool showBattlerayNotifications = false;
        public bool autoRefinement = false;
        public bool autoBoost = false;
        public bool autoChangeAmmo = false;

        public GameplaySettingsRequest(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false) {
            this.autoBoost = param1;
            this.autoRefinement = param2;
            this.quickslotStopAttack = param3;
            this.doubleclickAttack = param4;
            this.autoChangeAmmo = param5;
            this.autoStart = param6;
            this.autoBuyGreenBootyKeys = param7;
            this.showBattlerayNotifications = param8;
            this.var_5281 = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5281 = param1.ReadBoolean();
            param1.ReadShort();
            this.quickslotStopAttack = param1.ReadBoolean();
            param1.ReadShort();
            this.doubleclickAttack = param1.ReadBoolean();
            this.autoBuyGreenBootyKeys = param1.ReadBoolean();
            this.autoStart = param1.ReadBoolean();
            this.showBattlerayNotifications = param1.ReadBoolean();
            this.autoRefinement = param1.ReadBoolean();
            this.autoBoost = param1.ReadBoolean();
            this.autoChangeAmmo = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_5281);
            param1.WriteShort(3545);
            param1.WriteBoolean(this.quickslotStopAttack);
            param1.WriteShort(-11583);
            param1.WriteBoolean(this.doubleclickAttack);
            param1.WriteBoolean(this.autoBuyGreenBootyKeys);
            param1.WriteBoolean(this.autoStart);
            param1.WriteBoolean(this.showBattlerayNotifications);
            param1.WriteBoolean(this.autoRefinement);
            param1.WriteBoolean(this.autoBoost);
            param1.WriteBoolean(this.autoChangeAmmo);
        }
    }
}
