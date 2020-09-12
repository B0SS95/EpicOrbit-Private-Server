using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_466 : ICommand {

        // CTBBeaconPositionCommand
        // ClanMemberMapInfoCommand

        public short ID { get; set; } = 3815;
        public int var_1197 = 0;
        public int mapId = 0;

        public class_466(int param1 = 0, int param2 = 0) {
            this.var_1197 = param1;
            this.mapId = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_1197 = param1.ReadInt();
            this.var_1197 = param1.Shift(this.var_1197, 23);
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 2);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-7095);
            param1.WriteInt(param1.Shift(this.var_1197, 9));
            param1.WriteInt(param1.Shift(this.mapId, 30));
        }
    }
}
