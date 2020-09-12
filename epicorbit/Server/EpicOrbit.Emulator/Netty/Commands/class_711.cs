using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_711 : ICommand {

        // JumpCPUPowerupStatusCommand
        // JumpInitiatedCommand

        public short ID { get; set; } = 2410;
        public int mapId = 0;
        public int var_5275 = 0;

        public class_711(int param1 = 0, int param2 = 0) {
            this.mapId = param1;
            this.var_5275 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 2);
            this.var_5275 = param1.ReadInt();
            this.var_5275 = param1.Shift(this.var_5275, 20);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.mapId, 30));
            param1.WriteInt(param1.Shift(this.var_5275, 12));
        }
    }
}
