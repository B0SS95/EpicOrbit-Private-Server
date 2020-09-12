using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AssetCreateCommand : ICommand {

        public short ID { get; set; } = 23109;
        public int expansionStage = 0;
        public string clanTag = "";
        public AssetTypeModule type;
        public int factionId = 0;
        public int posY = 0;
        public ClanRelationModule clanRelation;
        public bool detectedByWarnRadar = false;
        public List<VisualModifierCommand> modifier;
        public int assetId = 0;
        public bool invisible = false;
        public bool showBubble = false;
        public int clanId = 0;
        public string userName = "";
        public int designId = 0;
        public int posX = 0;
        public bool visibleOnWarnRadar = false;

        public AssetCreateCommand(AssetTypeModule param1 = null, string param2 = "", int param3 = 0, string param4 = "", int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0, bool param11 = false, bool param12 = false, bool param13 = false, bool param14 = false, ClanRelationModule param15 = null, List<VisualModifierCommand> param16 = null) {
            if (param1 == null) {
                this.type = new AssetTypeModule();
            } else {
                this.type = param1;
            }
            this.userName = param2;
            this.factionId = param3;
            this.clanTag = param4;
            this.assetId = param5;
            this.designId = param6;
            this.expansionStage = param7;
            this.posX = param8;
            this.posY = param9;
            this.clanId = param10;
            this.invisible = param11;
            this.visibleOnWarnRadar = param12;
            this.detectedByWarnRadar = param13;
            this.showBubble = param14;
            if (param15 == null) {
                this.clanRelation = new ClanRelationModule();
            } else {
                this.clanRelation = param15;
            }
            if (param16 == null) {
                this.modifier = new List<VisualModifierCommand>();
            } else {
                this.modifier = param16;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.expansionStage = param1.ReadInt();
            this.expansionStage = param1.Shift(this.expansionStage, 18);
            this.clanTag = param1.ReadUTF();
            param1.ReadShort();
            this.type = lookup.Lookup(param1) as AssetTypeModule;
            this.type.Read(param1, lookup);
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 14);
            this.posY = param1.ReadInt();
            this.posY = param1.Shift(this.posY, 17);
            this.clanRelation = lookup.Lookup(param1) as ClanRelationModule;
            this.clanRelation.Read(param1, lookup);
            this.detectedByWarnRadar = param1.ReadBoolean();
            this.modifier.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as VisualModifierCommand;
                tmp_0.Read(param1, lookup);
                this.modifier.Add(tmp_0);
            }
            this.assetId = param1.ReadInt();
            this.assetId = param1.Shift(this.assetId, 11);
            this.invisible = param1.ReadBoolean();
            param1.ReadShort();
            this.showBubble = param1.ReadBoolean();
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 26);
            this.userName = param1.ReadUTF();
            this.designId = param1.ReadInt();
            this.designId = param1.Shift(this.designId, 18);
            this.posX = param1.ReadInt();
            this.posX = param1.Shift(this.posX, 9);
            this.visibleOnWarnRadar = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.expansionStage, 14));
            param1.WriteUTF(this.clanTag);
            param1.WriteShort(5960);
            this.type.Write(param1);
            param1.WriteInt(param1.Shift(this.factionId, 18));
            param1.WriteInt(param1.Shift(this.posY, 15));
            this.clanRelation.Write(param1);
            param1.WriteBoolean(this.detectedByWarnRadar);
            param1.WriteInt(this.modifier.Count);
            foreach (var tmp_0 in this.modifier) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.assetId, 21));
            param1.WriteBoolean(this.invisible);
            param1.WriteShort(-31290);
            param1.WriteBoolean(this.showBubble);
            param1.WriteInt(param1.Shift(this.clanId, 6));
            param1.WriteUTF(this.userName);
            param1.WriteInt(param1.Shift(this.designId, 14));
            param1.WriteInt(param1.Shift(this.posX, 23));
            param1.WriteBoolean(this.visibleOnWarnRadar);
        }
    }
}
