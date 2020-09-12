using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipCreateCommand : ICommand {

        public short ID { get; set; } = 14290;
        public string clanTag = "";
        public int factionId = 0;
        public int x = 0;
        public List<VisualModifierCommand> modifier;
        public int dailyRank = 0;
        public int y = 0;
        public int clanId = 0;
        public int userId = 0;
        public bool npc = false;
        public string typeId = "";
        public bool cloaked = false;
        public bool warnBox = false;
        public int expansionStage = 0;
        public int motherShipId = 0;
        public MinimapColor minimapColor; // maybe group/clan mode ... green name/yellow name
        public ClanRelationModule clanDiplomacy;
        public string userName = "";
        public string var_4950 = "";
        public bool useSystemFont = false;
        public int galaxyGatesDone = 0;
        public int positionIndex = 0;

        public ShipCreateCommand(int param1 = 0, string param2 = "", int param3 = 0, string param4 = "", string param5 = "", int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, bool param11 = false, ClanRelationModule param12 = null, int param13 = 0, bool param14 = false, bool param15 = false, bool param16 = false, int param17 = 0, int param18 = 0, string param19 = "", List<VisualModifierCommand> param20 = null, MinimapColor param21 = null) {
            this.userId = param1;
            this.typeId = param2;
            this.expansionStage = param3;
            this.clanTag = param4;
            this.userName = param5;
            this.x = param6;
            this.y = param7;
            this.factionId = param8;
            this.clanId = param9;
            this.dailyRank = param10;
            this.warnBox = param11;
            if (param12 == null) {
                this.clanDiplomacy = new ClanRelationModule();
            } else {
                this.clanDiplomacy = param12;
            }
            this.galaxyGatesDone = param13;
            this.useSystemFont = param14;
            this.npc = param15;
            this.cloaked = param16;
            this.motherShipId = param17;
            this.positionIndex = param18;
            this.var_4950 = param19;
            if (param20 == null) {
                this.modifier = new List<VisualModifierCommand>();
            } else {
                this.modifier = param20;
            }
            if (param21 == null) {
                this.minimapColor = new MinimapColor();
            } else {
                this.minimapColor = param21;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.clanTag = param1.ReadUTF();
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 17);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 15);
            param1.ReadShort();
            this.modifier.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as VisualModifierCommand;
                tmp_0.Read(param1, lookup);
                this.modifier.Add(tmp_0);
            }
            this.dailyRank = param1.ReadInt();
            this.dailyRank = param1.Shift(this.dailyRank, 22);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 4);
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 20);
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 2);
            this.npc = param1.ReadBoolean();
            this.typeId = param1.ReadUTF();
            this.cloaked = param1.ReadBoolean();
            this.warnBox = param1.ReadBoolean();
            this.expansionStage = param1.ReadInt();
            this.expansionStage = param1.Shift(this.expansionStage, 8);
            this.motherShipId = param1.ReadInt();
            this.motherShipId = param1.Shift(this.motherShipId, 23);
            this.minimapColor = lookup.Lookup(param1) as MinimapColor;
            this.minimapColor.Read(param1, lookup);
            this.clanDiplomacy = lookup.Lookup(param1) as ClanRelationModule;
            this.clanDiplomacy.Read(param1, lookup);
            this.userName = param1.ReadUTF();
            this.var_4950 = param1.ReadUTF();
            this.useSystemFont = param1.ReadBoolean();
            this.galaxyGatesDone = param1.ReadInt();
            this.galaxyGatesDone = param1.Shift(this.galaxyGatesDone, 4);
            this.positionIndex = param1.ReadInt();
            this.positionIndex = param1.Shift(this.positionIndex, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.clanTag);
            param1.WriteInt(param1.Shift(this.factionId, 15));
            param1.WriteInt(param1.Shift(this.x, 17));
            param1.WriteShort(28985);
            param1.WriteInt(this.modifier.Count);
            foreach (var tmp_0 in this.modifier) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.dailyRank, 10));
            param1.WriteInt(param1.Shift(this.y, 28));
            param1.WriteInt(param1.Shift(this.clanId, 12));
            param1.WriteInt(param1.Shift(this.userId, 30));
            param1.WriteBoolean(this.npc);
            param1.WriteUTF(this.typeId);
            param1.WriteBoolean(this.cloaked);
            param1.WriteBoolean(this.warnBox);
            param1.WriteInt(param1.Shift(this.expansionStage, 24));
            param1.WriteInt(param1.Shift(this.motherShipId, 9));
            this.minimapColor.Write(param1);
            this.clanDiplomacy.Write(param1);
            param1.WriteUTF(this.userName);
            param1.WriteUTF(this.var_4950);
            param1.WriteBoolean(this.useSystemFont);
            param1.WriteInt(param1.Shift(this.galaxyGatesDone, 28));
            param1.WriteInt(param1.Shift(this.positionIndex, 2));
        }
    }
}
