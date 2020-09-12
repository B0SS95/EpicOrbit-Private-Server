using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MoveCommand : ICommand {

        public short ID { get; set; } = 17101;
        public int y = 0;
        public int timeToTarget = 0;
        public int userId = 0;
        public int x = 0;

        public MoveCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.userId = param1;
            this.x = param2;
            this.y = param3;
            this.timeToTarget = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 4);
            this.timeToTarget = param1.ReadInt();
            this.timeToTarget = param1.Shift(this.timeToTarget, 16);
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 5);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 2);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.y, 28));
            param1.WriteInt(param1.Shift(this.timeToTarget, 16));
            param1.WriteInt(param1.Shift(this.userId, 27));
            param1.WriteInt(param1.Shift(this.x, 30));
        }
    }
}
