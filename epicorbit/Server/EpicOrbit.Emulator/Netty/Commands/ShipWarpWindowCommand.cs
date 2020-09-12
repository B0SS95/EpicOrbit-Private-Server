using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipWarpWindowCommand : ICommand {

        public short ID { get; set; } = 28599;
        public bool isNearSpacestation = false;
        public int jumpVoucherCount = 0;
        public List<ShipWarpModule> ships;
        public int uridium = 0;

        public ShipWarpWindowCommand(int param1 = 0, int param2 = 0, bool param3 = false, List<ShipWarpModule> param4 = null) {
            this.jumpVoucherCount = param1;
            this.uridium = param2;
            this.isNearSpacestation = param3;
            if (param4 == null) {
                this.ships = new List<ShipWarpModule>();
            } else {
                this.ships = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.isNearSpacestation = param1.ReadBoolean();
            this.jumpVoucherCount = param1.ReadInt();
            this.jumpVoucherCount = param1.Shift(this.jumpVoucherCount, 26);
            this.ships.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ShipWarpModule;
                tmp_0.Read(param1, lookup);
                this.ships.Add(tmp_0);
            }
            this.uridium = param1.ReadInt();
            this.uridium = param1.Shift(this.uridium, 23);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.isNearSpacestation);
            param1.WriteInt(param1.Shift(this.jumpVoucherCount, 6));
            param1.WriteInt(this.ships.Count);
            foreach (var tmp_0 in this.ships) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.uridium, 9));
            param1.WriteShort(24192);
            param1.WriteShort(-32176);
        }
    }
}
