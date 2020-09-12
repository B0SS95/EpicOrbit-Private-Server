using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapAssetAddBillboardCommand : ICommand {

        public short ID { get; set; } = 31961;
        public PartnerTypeModule partnerType;
        public int x = 0;
        public int uid = 0;
        public int y = 0;
        public string hash = "";
        public AssetTypeModule type;

        public MapAssetAddBillboardCommand(string param1 = "", AssetTypeModule param2 = null, PartnerTypeModule param3 = null, int param4 = 0, int param5 = 0, int param6 = 0) {
            this.hash = param1;
            if (param2 == null) {
                this.type = new AssetTypeModule();
            } else {
                this.type = param2;
            }
            if (param3 == null) {
                this.partnerType = new PartnerTypeModule();
            } else {
                this.partnerType = param3;
            }
            this.x = param4;
            this.y = param5;
            this.uid = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.partnerType = lookup.Lookup(param1) as PartnerTypeModule;
            this.partnerType.Read(param1, lookup);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 31);
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 30);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 17);
            param1.ReadShort();
            this.hash = param1.ReadUTF();
            this.type = lookup.Lookup(param1) as AssetTypeModule;
            this.type.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.partnerType.Write(param1);
            param1.WriteInt(param1.Shift(this.x, 1));
            param1.WriteInt(param1.Shift(this.uid, 2));
            param1.WriteInt(param1.Shift(this.y, 15));
            param1.WriteShort(-7532);
            param1.WriteUTF(this.hash);
            this.type.Write(param1);
        }
    }
}
