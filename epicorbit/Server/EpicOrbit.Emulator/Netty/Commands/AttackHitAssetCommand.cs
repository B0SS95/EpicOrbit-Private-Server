using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttackHitAssetCommand : ICommand {

        public short ID { get; set; } = 15206;
        public int assetId = 0;
        public int hitpointsNow = 0;
        public int hitpointsMax = 0;

        public AttackHitAssetCommand(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.assetId = param1;
            this.hitpointsMax = param2;
            this.hitpointsNow = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.assetId = param1.ReadInt();
            this.assetId = param1.Shift(this.assetId, 30);
            param1.ReadShort();
            this.hitpointsNow = param1.ReadInt();
            this.hitpointsNow = param1.Shift(this.hitpointsNow, 27);
            this.hitpointsMax = param1.ReadInt();
            this.hitpointsMax = param1.Shift(this.hitpointsMax, 21);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.assetId, 2));
            param1.WriteShort(24899);
            param1.WriteInt(param1.Shift(this.hitpointsNow, 5));
            param1.WriteInt(param1.Shift(this.hitpointsMax, 11));
        }
    }
}
