using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UICamLockToShipCommand : ICommand {

        public short ID { get; set; } = 16764;
        public int userId = 0;
        public float zoomFactor = 0;
        public float duration = 0;

        public UICamLockToShipCommand(int param1 = 0, float param2 = 0, float param3 = 0) {
            this.userId = param1;
            this.zoomFactor = param2;
            this.duration = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 15);
            this.zoomFactor = param1.ReadFloat();
            param1.ReadShort();
            this.duration = param1.ReadFloat();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.userId, 17));
            param1.WriteFloat(this.zoomFactor);
            param1.WriteShort(6544);
            param1.WriteFloat(this.duration);
        }
    }
}
