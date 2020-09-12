using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestConditionModule : ICommand {

        public const short VISIT_QUEST_GIVER = 59;
        public const short RESTRICT_AMMUNITION_KILL_NPC = 56;
        public const short LEVEL = 50;
        public const short COLLECT_BONUS_BOX = 52;
        public const short USE_ORE_UPDATE = 68;
        public const short FUEL_SHORTAGE = 14;
        public const short const_3251 = 67;
        public const short IN_CLAN = 62;
        public const short ACTIVATE_MAP_ASSET_TYPE = 70;
        public const short SPEND_AMMUNITION = 22;
        public const short COORDINATES = 11;
        public const short VISIT_MAP = 31;
        public const short const_1580 = 51;
        public const short AMMUNITION = 20;
        public const short MISCELLANEOUS = 19;
        public const short DAMAGE = 7;
        public const short COLLECT = 5;
        public const short FINISH_GALAXY_GATE = 75;
        public const short WEB = 46;
        public const short DAMAGE_PLAYERS = 30;
        public const short GAIN_INFLUENCE = 76;
        public const short AVOID_DAMAGE = 8;
        public const short const_1417 = 53;
        public const short ENTER_GROUP = 55;
        public const short KILL_NPCS = 27;
        public const short QUICK_BUY = 54;
        public const short const_909 = 73;
        public const short AVOID_KILL_NPC = 33;
        public const short SALVAGE = 23;
        public const short AVOID_JUMP = 40;
        public const short AVOID_KILL_NPCS = 34;
        public const short SELL_ORE = 49;
        public const short VISIT_JUMP_GATE_TO_MAP_TYPE = 69;
        public const short DAMAGE_NPCS = 29;
        public const short STAY_AWAY = 43;
        public const short TRAVEL = 13;
        public const short JUMP = 39;
        public const short KILL_PLAYERS = 28;
        public const short STEAL = 24;
        public const short UPDATE_SKYLAB_TO_LEVEL = 72;
        public const short HASTE = 2;
        public const short EMPTY = 18;
        public const short AVOID_DAMAGE_NPCS = 36;
        public const short COLLECT_BUILT_RECIPE = 80;
        public const short REAL_TIME_HASTE = 60;
        public const short const_1944 = 77;
        public const short const_1094 = 78;
        public const short MAP = 16;
        public const short REFINE_ORE = 65;
        public const short const_1388 = 0;
        public const short PREVENT = 38;
        public const short IN_GROUP = 44;
        public const short STEADINESS = 41;
        public const short KILL_ANY = 45;
        public const short const_3132 = 58;
        public const short REAL_TIME_DATE_HASTE = 79;
        public const short RESTRICT_AMMUNITION_KILL_PLAYER = 57;
        public const short DISTANCE = 12;
        public const short KILL_NPC = 6;
        public const short CARGO = 48;
        public const short AVOID_DEATH = 10;
        public const short DIE = 32;
        public const short FINISH_STARTER_GATE = 64;
        public const short const_1714 = 26;
        public const short MULTIPLIER = 42;
        public const short SAVE_AMMUNITION = 21;
        public const short CLIENT = 47;
        public const short const_611 = 25;
        public const short TAKE_DAMAGE = 9;
        public const short PROXIMITY = 15;
        public const short COUNTDOWN = 4;
        public const short TIMER = 1;
        public const short AVOID_DAMAGE_PLAYERS = 37;
        public const short MAP_DIVERSE = 17;
        public const short ENDURANCE = 3;
        public const short VISIT_MAP_ASSET = 71;
        public const short COLLECT_LOOT = 63;
        public const short const_229 = 61;
        public const short BEACON_TAKEOVER = 74;
        public const short AVOID_KILL_PLAYERS = 35;
        public const short PUT_ITEM_IN_SLOT_BAR = 66;
        public short ID { get; set; } = 29943;
        public double targetValue = 0;
        public int id = 0;
        public bool mandatory = false;
        public List<QuestConditionModule> subConditions;
        public QuestConditionStateModule state;
        public short type = 0;
        public List<string> matches;
        public short displayType = 0;

        public QuestConditionModule(int param1 = 0, List<string> param2 = null, short param3 = 0, short param4 = 0, double param5 = 0, bool param6 = false, QuestConditionStateModule param7 = null, List<QuestConditionModule> param8 = null) {
            this.id = param1;
            if (param2 == null) {
                this.matches = new List<String>();
            } else {
                this.matches = param2;
            }
            this.type = param3;
            this.displayType = param4;
            this.targetValue = param5;
            this.mandatory = param6;
            if (param7 == null) {
                this.state = new QuestConditionStateModule();
            } else {
                this.state = param7;
            }
            if (param8 == null) {
                this.subConditions = new List<QuestConditionModule>();
            } else {
                this.subConditions = param8;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.targetValue = param1.ReadDouble();
            param1.ReadShort();
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 7);
            this.mandatory = param1.ReadBoolean();
            this.subConditions.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestConditionModule;
                tmp_0.Read(param1, lookup);
                this.subConditions.Add(tmp_0);
            }
            this.state = lookup.Lookup(param1) as QuestConditionStateModule;
            this.state.Read(param1, lookup);
            this.type = param1.ReadShort();
            this.matches.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.ReadUTF();
                this.matches.Add(tmp_0);
            }
            this.displayType = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.targetValue);
            param1.WriteShort(2001);
            param1.WriteInt(param1.Shift(this.id, 25));
            param1.WriteBoolean(this.mandatory);
            param1.WriteInt(this.subConditions.Count);
            foreach (var tmp_0 in this.subConditions) {
                tmp_0.Write(param1);
            }
            this.state.Write(param1);
            param1.WriteShort(this.type);
            param1.WriteInt(this.matches.Count);
            foreach (var tmp_0 in this.matches) {
                param1.WriteUTF(tmp_0);
            }
            param1.WriteShort(this.displayType);
            param1.WriteShort(2039);
        }
    }
}
