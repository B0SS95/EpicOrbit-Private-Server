using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetHeroActivationCommand : ICommand {

        public short ID { get; set; } = 23933;
        public MinimapColor var_4452;
        public int ownerId = 0;
        public int x = 0;
        public int petSpeed = 0;
        public int petId = 0;
        public string clanTag = "";
        public short petLevel = 0;
        public int clanId = 0;
        public short factionId = 0;
        public string petName = "";
        public short shipType = 0;
        public short expansionStage = 0;
        public int y = 0;

        public PetHeroActivationCommand(int param1 = 0, int param2 = 0, short param3 = 0, short param4 = 0, string param5 = "", short param6 = 0, int param7 = 0, short param8 = 0, string param9 = "", int param10 = 0, int param11 = 0, int param12 = 0, MinimapColor param13 = null) {
            this.ownerId = param1;
            this.petId = param2;
            this.shipType = param3;
            this.expansionStage = param4;
            this.petName = param5;
            this.factionId = param6;
            this.clanId = param7;
            this.petLevel = param8;
            this.clanTag = param9;
            this.x = param10;
            this.y = param11;
            this.petSpeed = param12;
            if (param13 == null) {
                this.var_4452 = new MinimapColor();
            } else {
                this.var_4452 = param13;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4452 = lookup.Lookup(param1) as MinimapColor;
            this.var_4452.Read(param1, lookup);
            this.ownerId = param1.ReadInt();
            this.ownerId = param1.Shift(this.ownerId, 19);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 6);
            this.petSpeed = param1.ReadInt();
            this.petSpeed = param1.Shift(this.petSpeed, 31);
            this.petId = param1.ReadInt();
            this.petId = param1.Shift(this.petId, 22);
            this.clanTag = param1.ReadUTF();
            this.petLevel = param1.ReadShort();
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 11);
            this.factionId = param1.ReadShort();
            this.petName = param1.ReadUTF();
            this.shipType = param1.ReadShort();
            this.expansionStage = param1.ReadShort();
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_4452.Write(param1);
            param1.WriteInt(param1.Shift(this.ownerId, 13));
            param1.WriteInt(param1.Shift(this.x, 26));
            param1.WriteInt(param1.Shift(this.petSpeed, 1));
            param1.WriteInt(param1.Shift(this.petId, 10));
            param1.WriteUTF(this.clanTag);
            param1.WriteShort(this.petLevel);
            param1.WriteInt(param1.Shift(this.clanId, 21));
            param1.WriteShort(this.factionId);
            param1.WriteUTF(this.petName);
            param1.WriteShort(this.shipType);
            param1.WriteShort(this.expansionStage);
            param1.WriteInt(param1.Shift(this.y, 13));
        }
    }
}
