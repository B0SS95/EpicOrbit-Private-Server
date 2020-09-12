using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_472 : ICommand {

        // ClanMemberMapInfoCommand
        // CTBBeaconCapturedCommand
        // CTBBeaconReleasedCommand
        // ShipExtensionsCommand

        public short ID { get; set; } = 3114;
        public int userId = 0;
        public int var_1197 = 0;

        public class_472(int param1 = 0, int param2 = 0) {
            this.userId = param1;
            this.var_1197 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 4);
            this.var_1197 = param1.ReadInt();
            this.var_1197 = param1.Shift(this.var_1197, 4);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.userId, 28));
            param1.WriteInt(param1.Shift(this.var_1197, 28));
            param1.WriteShort(18180);
            param1.WriteShort(-28573);
        }
    }
}
