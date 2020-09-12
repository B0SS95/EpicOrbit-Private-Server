using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_882 : ICommand {

        // ClanMemberMapInfoCommand
        // CTBBeaconPositionCommand
        // JumpCPUPowerupStatusCommand
        // JumpInitiatedCommand

        public short ID { get; set; } = 31285;
        public int var_294 = 0;
        public int mapID = 0;

        public class_882(int param1 = 0, int param2 = 0) {
            this.mapID = param1;
            this.var_294 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_294 = param1.ReadInt();
            this.var_294 = param1.Shift(this.var_294, 22);
            this.mapID = param1.ReadInt();
            this.mapID = param1.Shift(this.mapID, 16);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(17752);
            param1.WriteInt(param1.Shift(this.var_294, 10));
            param1.WriteInt(param1.Shift(this.mapID, 16));
        }
    }
}
