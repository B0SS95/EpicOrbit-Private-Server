using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipWarpRequest : ICommand {

        public short ID { get; set; } = 29884;
        public int shipId = 0;

        public ShipWarpRequest(int param1 = 0) {
            this.shipId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.shipId = param1.ReadInt();
            this.shipId = param1.Shift(this.shipId, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(10651);
            param1.WriteShort(15664);
            param1.WriteInt(param1.Shift(this.shipId, 18));
        }
    }
}
