using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_775 : ICommand {

        // Diese scheiﬂ videos die von der seite aufpoppen und dir sagen wollen wie man spielt xD

        public const short UNKOWN_DANGERS = 18;
        public const short TECH_FACTORY = 26;
        public const short SHIP_DESIGN = 32;
        public const short CHANGING_SHIPS = 4;
        public const short PREPARE_BATTLE = 7;
        public const short INSTALLING_NEW_EQUIPMENT = 5;
        public const short TRAINING_GROUNDS = 36;
        public const short ROCKET_LAUNCHER = 30;
        public const short const_1175 = 15;
        public const short ITEM_UPGRADE = 23;
        public const short SHIP_REPAIR = 0;
        public const short SELL_RESOURCE = 10;
        public const short THE_SHOP = 3;
        public const short SECOND_CONFIGURATION = 22;
        public const short PALLADIUM = 28;
        public const short CONTACT_LIST = 34;
        public const short ORE_TRANSFER = 35;
        public const short CLAN_BATTLE_STATION = 27;
        public const short REQUEST_MISSION = 14;
        public const short POLICY_CHANGES = 16;
        public const short AUCTION_HOUSE = 29;
        public const short BOOST_YOUR_EQUIP = 9;
        public const short PVP_WARNING = 2;
        public const short HOW_TO_FLY = 13;
        public const short ASSEMBLY = 39;
        public const short ATTACK = 19;
        public const short const_2980 = 37;
        public const short WELCOME = 12;
        public const short JUMP_DEVICE = 20;
        public const short EXTRA_CPU = 31;
        public const short LOOKING_FOR_GROUPS = 33;
        public const short MOTIVATIONAL_SURVEY = 38;
        public const short JUMP_GATES = 6;
        public const short EQUIP_YOUR_ROCKETS = 17;
        public const short WEALTHY_FAMOUS = 11;
        public const short SKILL_TREE = 25;
        public const short GALAXY_GATE = 24;
        public const short FULL_CARGO = 21;
        public const short SKYLAB = 1;
        public const short GET_MORE_AMMO = 8;
        public short ID { get; set; } = 28291;
        public short content = 0;

        public class_775(short param1 = 0) {
            this.content = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.content = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.content);
            param1.WriteShort(-3851);
        }
    }
}
