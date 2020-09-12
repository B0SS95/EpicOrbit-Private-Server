using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipInitializationCommand : ICommand {

        public short ID { get; set; } = 7902;
        public int expansionStage = 0;
        public int level = 0;
        public int y = 0;
        public int hitMax = 0;
        public List<VisualModifierCommand> modifier;
        public int factionId = 0;
        public int shield = 0;
        public int shieldMax = 0;
        public double credits = 0;
        public int userId = 0;
        public int hitPoints = 0;
        public bool premium = false;
        public double honourPoints = 0;
        public double uridium = 0;
        public int cargoSpaceMax = 0;
        public int mapId = 0;
        public int galaxyGatesDone = 0;
        public string userName = "";
        public int x = 0;
        public bool cloaked = false;
        public string typeId = "";
        public int maxNanoHull = 0;
        public int dailyRank = 0;
        public int cargoSpace = 0;
        public float jackpot = 0;
        public bool useSystemFont = false;
        public bool var_5074 = false;
        public string clanTag = "";
        public int clanId = 0;
        public double ep = 0;
        public int nanoHull = 0;
        public int speed = 0;

        public ShipInitializationCommand(int param1 = 0, string param2 = "", string param3 = "", int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, int param11 = 0, int param12 = 0, int param13 = 0, int param14 = 0, int param15 = 0, int param16 = 0, int param17 = 0, int param18 = 0, bool param19 = false, double param20 = 0, double param21 = 0, int param22 = 0, double param23 = 0, double param24 = 0, float param25 = 0, int param26 = 0, string param27 = "", int param28 = 0, bool param29 = false, bool param30 = false, bool param31 = false, List<VisualModifierCommand> param32 = null) {
            this.userId = param1;
            this.userName = param2;
            this.typeId = param3;
            this.speed = param4;
            this.shield = param5;
            this.shieldMax = param6;
            this.hitPoints = param7;
            this.hitMax = param8;
            this.cargoSpace = param9;
            this.cargoSpaceMax = param10;
            this.nanoHull = param11;
            this.maxNanoHull = param12;
            this.x = param13;
            this.y = param14;
            this.mapId = param15;
            this.factionId = param16;
            this.clanId = param17;
            this.expansionStage = param18;
            this.premium = param19;
            this.ep = param20;
            this.honourPoints = param21;
            this.level = param22;
            this.credits = param23;
            this.uridium = param24;
            this.jackpot = param25;
            this.dailyRank = param26;
            this.clanTag = param27;
            this.galaxyGatesDone = param28;
            this.useSystemFont = param29;
            this.cloaked = param30;
            this.var_5074 = param31;
            if (param32 == null) {
                this.modifier = new List<VisualModifierCommand>();
            } else {
                this.modifier = param32;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.expansionStage = param1.ReadInt();
            this.expansionStage = param1.Shift(this.expansionStage, 26);
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 16);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 18);
            param1.ReadShort();
            param1.ReadShort();
            this.hitMax = param1.ReadInt();
            this.hitMax = param1.Shift(this.hitMax, 13);
            this.modifier.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as VisualModifierCommand;
                tmp_0.Read(param1, lookup);
                this.modifier.Add(tmp_0);
            }
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 15);
            this.shield = param1.ReadInt();
            this.shield = param1.Shift(this.shield, 10);
            this.shieldMax = param1.ReadInt();
            this.shieldMax = param1.Shift(this.shieldMax, 24);
            this.credits = param1.ReadDouble();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 18);
            this.hitPoints = param1.ReadInt();
            this.hitPoints = param1.Shift(this.hitPoints, 15);
            this.premium = param1.ReadBoolean();
            this.honourPoints = param1.ReadDouble();
            this.uridium = param1.ReadDouble();
            this.cargoSpaceMax = param1.ReadInt();
            this.cargoSpaceMax = param1.Shift(this.cargoSpaceMax, 30);
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 21);
            this.galaxyGatesDone = param1.ReadInt();
            this.galaxyGatesDone = param1.Shift(this.galaxyGatesDone, 8);
            this.userName = param1.ReadUTF();
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 1);
            this.cloaked = param1.ReadBoolean();
            this.typeId = param1.ReadUTF();
            this.maxNanoHull = param1.ReadInt();
            this.maxNanoHull = param1.Shift(this.maxNanoHull, 18);
            this.dailyRank = param1.ReadInt();
            this.dailyRank = param1.Shift(this.dailyRank, 16);
            this.cargoSpace = param1.ReadInt();
            this.cargoSpace = param1.Shift(this.cargoSpace, 15);
            this.jackpot = param1.ReadFloat();
            this.useSystemFont = param1.ReadBoolean();
            this.var_5074 = param1.ReadBoolean();
            this.clanTag = param1.ReadUTF();
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 17);
            this.ep = param1.ReadDouble();
            this.nanoHull = param1.ReadInt();
            this.nanoHull = param1.Shift(this.nanoHull, 11);
            this.speed = param1.ReadInt();
            this.speed = param1.Shift(this.speed, 16);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.expansionStage, 6));
            param1.WriteInt(param1.Shift(this.level, 16));
            param1.WriteInt(param1.Shift(this.y, 14));
            param1.WriteShort(-23705);
            param1.WriteShort(134);
            param1.WriteInt(param1.Shift(this.hitMax, 19));
            param1.WriteInt(this.modifier.Count);
            foreach (var tmp_0 in this.modifier) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.factionId, 17));
            param1.WriteInt(param1.Shift(this.shield, 22));
            param1.WriteInt(param1.Shift(this.shieldMax, 8));
            param1.WriteDouble(this.credits);
            param1.WriteInt(param1.Shift(this.userId, 14));
            param1.WriteInt(param1.Shift(this.hitPoints, 17));
            param1.WriteBoolean(this.premium);
            param1.WriteDouble(this.honourPoints);
            param1.WriteDouble(this.uridium);
            param1.WriteInt(param1.Shift(this.cargoSpaceMax, 2));
            param1.WriteInt(param1.Shift(this.mapId, 11));
            param1.WriteInt(param1.Shift(this.galaxyGatesDone, 24));
            param1.WriteUTF(this.userName);
            param1.WriteInt(param1.Shift(this.x, 31));
            param1.WriteBoolean(this.cloaked);
            param1.WriteUTF(this.typeId);
            param1.WriteInt(param1.Shift(this.maxNanoHull, 14));
            param1.WriteInt(param1.Shift(this.dailyRank, 16));
            param1.WriteInt(param1.Shift(this.cargoSpace, 17));
            param1.WriteFloat(this.jackpot);
            param1.WriteBoolean(this.useSystemFont);
            param1.WriteBoolean(this.var_5074);
            param1.WriteUTF(this.clanTag);
            param1.WriteInt(param1.Shift(this.clanId, 15));
            param1.WriteDouble(this.ep);
            param1.WriteInt(param1.Shift(this.nanoHull, 21));
            param1.WriteInt(param1.Shift(this.speed, 16));
        }
    }
}
