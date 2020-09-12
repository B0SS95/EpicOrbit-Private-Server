using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetActivationCommand : ICommand {

        public short ID { get; set; } = 16267;
        public short petFactionId = 0;
        public bool isInIdleMode = false;
        public int petId = 0;
        public string petName = "";
        public string clanTag = "";
        public int petClanID = 0;
        public int petSpeed = 0;
        public MinimapColor var_4452;
        public int x = 0;
        public int y = 0;
        public ClanRelationModule clanRelationship;
        public short petDesignId = 0;
        public short petLevel = 0;
        public int ownerId = 0;
        public bool isVisible = false;
        public short expansionStage = 0;

        public PetActivationCommand(int param1 = 0, int param2 = 0, short param3 = 0, short param4 = 0, string param5 = "", short param6 = 0, int param7 = 0, short param8 = 0, string param9 = "", ClanRelationModule param10 = null, int param11 = 0, int param12 = 0, int param13 = 0, bool param14 = false, bool param15 = false, MinimapColor param16 = null) {
            this.ownerId = param1;
            this.petId = param2;
            this.petDesignId = param3;
            this.expansionStage = param4;
            this.petName = param5;
            this.petFactionId = param6;
            this.petClanID = param7;
            this.petLevel = param8;
            this.clanTag = param9;
            if (param10 == null) {
                this.clanRelationship = new ClanRelationModule();
            } else {
                this.clanRelationship = param10;
            }
            this.x = param11;
            this.y = param12;
            this.petSpeed = param13;
            this.isInIdleMode = param14;
            this.isVisible = param15;
            if (param16 == null) {
                this.var_4452 = new MinimapColor();
            } else {
                this.var_4452 = param16;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.petFactionId = param1.ReadShort();
            param1.ReadShort();
            this.isInIdleMode = param1.ReadBoolean();
            this.petId = param1.ReadInt();
            this.petId = param1.Shift(this.petId, 16);
            this.petName = param1.ReadUTF();
            this.clanTag = param1.ReadUTF();
            this.petClanID = param1.ReadInt();
            this.petClanID = param1.Shift(this.petClanID, 27);
            this.petSpeed = param1.ReadInt();
            this.petSpeed = param1.Shift(this.petSpeed, 20);
            this.var_4452 = lookup.Lookup(param1) as MinimapColor;
            this.var_4452.Read(param1, lookup);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 22);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 23);
            this.clanRelationship = lookup.Lookup(param1) as ClanRelationModule;
            this.clanRelationship.Read(param1, lookup);
            this.petDesignId = param1.ReadShort();
            this.petLevel = param1.ReadShort();
            this.ownerId = param1.ReadInt();
            this.ownerId = param1.Shift(this.ownerId, 30);
            this.isVisible = param1.ReadBoolean();
            this.expansionStage = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.petFactionId);
            param1.WriteShort(-14483);
            param1.WriteBoolean(this.isInIdleMode);
            param1.WriteInt(param1.Shift(this.petId, 16));
            param1.WriteUTF(this.petName);
            param1.WriteUTF(this.clanTag);
            param1.WriteInt(param1.Shift(this.petClanID, 5));
            param1.WriteInt(param1.Shift(this.petSpeed, 12));
            this.var_4452.Write(param1);
            param1.WriteInt(param1.Shift(this.x, 10));
            param1.WriteInt(param1.Shift(this.y, 9));
            this.clanRelationship.Write(param1);
            param1.WriteShort(this.petDesignId);
            param1.WriteShort(this.petLevel);
            param1.WriteInt(param1.Shift(this.ownerId, 2));
            param1.WriteBoolean(this.isVisible);
            param1.WriteShort(this.expansionStage);
        }
    }
}
