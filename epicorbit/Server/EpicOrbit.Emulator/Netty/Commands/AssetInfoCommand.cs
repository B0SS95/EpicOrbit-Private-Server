using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AssetInfoCommand : ICommand {

        public short ID { get; set; } = 27979;
        public int shieldEnergy = 0;
        public bool shielded = false;
        public int hitpoints = 0;
        public int maxHitpoints = 0;
        public int assetId = 0;
        public int maxShieldEnergy = 0;
        public AssetTypeModule type;
        public int designId = 0;
        public int expansionStage = 0;

        public AssetInfoCommand(int param1 = 0, AssetTypeModule param2 = null, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, bool param7 = false, int param8 = 0, int param9 = 0) {
            this.assetId = param1;
            if (param2 == null) {
                this.type = new AssetTypeModule();
            } else {
                this.type = param2;
            }
            this.designId = param3;
            this.expansionStage = param4;
            this.hitpoints = param5;
            this.maxHitpoints = param6;
            this.shielded = param7;
            this.shieldEnergy = param8;
            this.maxShieldEnergy = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.shieldEnergy = param1.ReadInt();
            this.shieldEnergy = param1.Shift(this.shieldEnergy, 16);
            this.shielded = param1.ReadBoolean();
            this.hitpoints = param1.ReadInt();
            this.hitpoints = param1.Shift(this.hitpoints, 7);
            this.maxHitpoints = param1.ReadInt();
            this.maxHitpoints = param1.Shift(this.maxHitpoints, 15);
            this.assetId = param1.ReadInt();
            this.assetId = param1.Shift(this.assetId, 8);
            this.maxShieldEnergy = param1.ReadInt();
            this.maxShieldEnergy = param1.Shift(this.maxShieldEnergy, 5);
            this.type = lookup.Lookup(param1) as AssetTypeModule;
            this.type.Read(param1, lookup);
            this.designId = param1.ReadInt();
            this.designId = param1.Shift(this.designId, 22);
            this.expansionStage = param1.ReadInt();
            this.expansionStage = param1.Shift(this.expansionStage, 15);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(16363);
            param1.WriteInt(param1.Shift(this.shieldEnergy, 16));
            param1.WriteBoolean(this.shielded);
            param1.WriteInt(param1.Shift(this.hitpoints, 25));
            param1.WriteInt(param1.Shift(this.maxHitpoints, 17));
            param1.WriteInt(param1.Shift(this.assetId, 24));
            param1.WriteInt(param1.Shift(this.maxShieldEnergy, 27));
            this.type.Write(param1);
            param1.WriteInt(param1.Shift(this.designId, 10));
            param1.WriteInt(param1.Shift(this.expansionStage, 17));
        }
    }
}
