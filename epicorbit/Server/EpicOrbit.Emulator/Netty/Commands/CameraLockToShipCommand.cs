using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CameraLockToShipCommand : ICommand {

        public short ID { get; set; } = 25702;
        public float zoomFactor = 0;
        public float duration = 0;
        public int lockedShipUserID = 0;

        public CameraLockToShipCommand(int param1 = 0, float param2 = 0, float param3 = 0) {
            this.lockedShipUserID = param1;
            this.zoomFactor = param2;
            this.duration = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.zoomFactor = param1.ReadFloat();
            this.duration = param1.ReadFloat();
            param1.ReadShort();
            this.lockedShipUserID = param1.ReadInt();
            this.lockedShipUserID = param1.Shift(this.lockedShipUserID, 21);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteFloat(this.zoomFactor);
            param1.WriteFloat(this.duration);
            param1.WriteShort(-29015);
            param1.WriteInt(param1.Shift(this.lockedShipUserID, 11));
            param1.WriteShort(-15775);
        }
    }
}
