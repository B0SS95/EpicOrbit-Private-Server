using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_762 : ICommand {

        // CameraLockToCoordinatesCommand
        // UICamLockToCoordinatesCommand

        public short ID { get; set; } = 5204;
        public int x = 0;
        public int y = 0;
        public float duration = 0;

        public class_762(int param1 = 0, int param2 = 0, float param3 = 0) {
            this.x = param1;
            this.y = param2;
            this.duration = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 12);
            param1.ReadShort();
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 9);
            this.duration = param1.ReadFloat();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.x, 20));
            param1.WriteShort(25790);
            param1.WriteInt(param1.Shift(this.y, 23));
            param1.WriteFloat(this.duration);
        }
    }
}
