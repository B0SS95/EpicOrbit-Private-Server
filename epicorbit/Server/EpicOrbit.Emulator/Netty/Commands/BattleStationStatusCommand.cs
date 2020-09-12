using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class BattleStationStatusCommand : ICommand {

        public short ID { get; set; } = 8566;
        public int attackRating = 0;
        public int damageBoosterRating = 0;
        public string battleStationName = "";
        public int honorBoosterRating = 0;
        public int battleStationId = 0;
        public int deflectorShieldSecondsMax = 0;
        public int mapAssetId = 0;
        public int repairRating = 0;
        public int repairPrice = 0;
        public int defenceRating = 0;
        public int experienceBoosterRating = 0;
        public int deflectorShieldSeconds = 0;
        public EquippedModulesModule equipment;
        public bool deflectorShieldActive = false;
        public int deflectorShieldRate = 0;
        public bool var_4522 = false;

        public BattleStationStatusCommand(int param1 = 0, int param2 = 0, string param3 = "", bool param4 = false, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, int param11 = 0, int param12 = 0, int param13 = 0, int param14 = 0, EquippedModulesModule param15 = null, bool param16 = false) {
            this.mapAssetId = param1;
            this.battleStationId = param2;
            this.battleStationName = param3;
            this.deflectorShieldActive = param4;
            this.deflectorShieldSeconds = param5;
            this.deflectorShieldSecondsMax = param6;
            this.attackRating = param7;
            this.defenceRating = param8;
            this.repairRating = param9;
            this.honorBoosterRating = param10;
            this.experienceBoosterRating = param11;
            this.damageBoosterRating = param12;
            this.deflectorShieldRate = param13;
            this.repairPrice = param14;
            if (param15 == null) {
                this.equipment = new EquippedModulesModule();
            } else {
                this.equipment = param15;
            }
            this.var_4522 = param16;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.attackRating = param1.ReadInt();
            this.attackRating = param1.Shift(this.attackRating, 27);
            this.damageBoosterRating = param1.ReadInt();
            this.damageBoosterRating = param1.Shift(this.damageBoosterRating, 14);
            this.battleStationName = param1.ReadUTF();
            this.honorBoosterRating = param1.ReadInt();
            this.honorBoosterRating = param1.Shift(this.honorBoosterRating, 6);
            param1.ReadShort();
            this.battleStationId = param1.ReadInt();
            this.battleStationId = param1.Shift(this.battleStationId, 21);
            this.deflectorShieldSecondsMax = param1.ReadInt();
            this.deflectorShieldSecondsMax = param1.Shift(this.deflectorShieldSecondsMax, 2);
            this.mapAssetId = param1.ReadInt();
            this.mapAssetId = param1.Shift(this.mapAssetId, 23);
            this.repairRating = param1.ReadInt();
            this.repairRating = param1.Shift(this.repairRating, 5);
            this.repairPrice = param1.ReadInt();
            this.repairPrice = param1.Shift(this.repairPrice, 30);
            this.defenceRating = param1.ReadInt();
            this.defenceRating = param1.Shift(this.defenceRating, 1);
            this.experienceBoosterRating = param1.ReadInt();
            this.experienceBoosterRating = param1.Shift(this.experienceBoosterRating, 17);
            this.deflectorShieldSeconds = param1.ReadInt();
            this.deflectorShieldSeconds = param1.Shift(this.deflectorShieldSeconds, 9);
            this.equipment = lookup.Lookup(param1) as EquippedModulesModule;
            this.equipment.Read(param1, lookup);
            this.deflectorShieldActive = param1.ReadBoolean();
            this.deflectorShieldRate = param1.ReadInt();
            this.deflectorShieldRate = param1.Shift(this.deflectorShieldRate, 5);
            this.var_4522 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.attackRating, 5));
            param1.WriteInt(param1.Shift(this.damageBoosterRating, 18));
            param1.WriteUTF(this.battleStationName);
            param1.WriteInt(param1.Shift(this.honorBoosterRating, 26));
            param1.WriteShort(-19520);
            param1.WriteInt(param1.Shift(this.battleStationId, 11));
            param1.WriteInt(param1.Shift(this.deflectorShieldSecondsMax, 30));
            param1.WriteInt(param1.Shift(this.mapAssetId, 9));
            param1.WriteInt(param1.Shift(this.repairRating, 27));
            param1.WriteInt(param1.Shift(this.repairPrice, 2));
            param1.WriteInt(param1.Shift(this.defenceRating, 31));
            param1.WriteInt(param1.Shift(this.experienceBoosterRating, 15));
            param1.WriteInt(param1.Shift(this.deflectorShieldSeconds, 23));
            this.equipment.Write(param1);
            param1.WriteBoolean(this.deflectorShieldActive);
            param1.WriteInt(param1.Shift(this.deflectorShieldRate, 27));
            param1.WriteBoolean(this.var_4522);
        }
    }
}
