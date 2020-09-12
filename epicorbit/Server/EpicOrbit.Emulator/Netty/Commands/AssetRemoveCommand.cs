using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AssetRemoveCommand : ICommand {

        public short ID { get; set; } = 25968;
        public AssetTypeModule assetType;
        public int uid = 0;

        public AssetRemoveCommand(AssetTypeModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.assetType = new AssetTypeModule();
            } else {
                this.assetType = param1;
            }
            this.uid = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.assetType = lookup.Lookup(param1) as AssetTypeModule;
            this.assetType.Read(param1, lookup);
            this.uid = param1.ReadInt();
            this.uid = param1.Shift(this.uid, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-20218);
            this.assetType.Write(param1);
            param1.WriteInt(param1.Shift(this.uid, 29));
        }
    }
}
