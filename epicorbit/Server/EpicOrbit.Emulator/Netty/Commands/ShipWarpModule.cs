using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ShipWarpModule : ICommand {

        public short ID { get; set; } = 8803;
        public string typeId = "";
        public int voucherPrice = 0;
        public int hangarId = 0;
        public int shipId = 0;
        public string hangarName = "";
        public int uridiumPrice = 0;
        public string shipDesignName = "";

        public ShipWarpModule(int param1 = 0, string param2 = "", string param3 = "", int param4 = 0, int param5 = 0, int param6 = 0, string param7 = "") {
            this.shipId = param1;
            this.typeId = param2;
            this.shipDesignName = param3;
            this.uridiumPrice = param4;
            this.voucherPrice = param5;
            this.hangarId = param6;
            this.hangarName = param7;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeId = param1.ReadUTF();
            param1.ReadShort();
            this.voucherPrice = param1.ReadInt();
            this.voucherPrice = param1.Shift(this.voucherPrice, 15);
            this.hangarId = param1.ReadInt();
            this.hangarId = param1.Shift(this.hangarId, 29);
            this.shipId = param1.ReadInt();
            this.shipId = param1.Shift(this.shipId, 31);
            this.hangarName = param1.ReadUTF();
            this.uridiumPrice = param1.ReadInt();
            this.uridiumPrice = param1.Shift(this.uridiumPrice, 7);
            this.shipDesignName = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.typeId);
            param1.WriteShort(10535);
            param1.WriteInt(param1.Shift(this.voucherPrice, 17));
            param1.WriteInt(param1.Shift(this.hangarId, 3));
            param1.WriteInt(param1.Shift(this.shipId, 1));
            param1.WriteUTF(this.hangarName);
            param1.WriteInt(param1.Shift(this.uridiumPrice, 25));
            param1.WriteUTF(this.shipDesignName);
        }
    }
}
