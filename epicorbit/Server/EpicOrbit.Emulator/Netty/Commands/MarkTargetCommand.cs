using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MarkTargetCommand : ICommand {

        public short ID { get; set; } = 26384;
        public int positionX = 0;
        public int positionY = 0;
        public int targetId = 0;
        public int userId = 0;

        public MarkTargetCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.userId = param1;
            this.targetId = param2;
            this.positionX = param3;
            this.positionY = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.positionX = param1.ReadInt();
            this.positionX = param1.Shift(this.positionX, 23);
            this.positionY = param1.ReadInt();
            this.positionY = param1.Shift(this.positionY, 29);
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 2);
            param1.ReadShort();
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.positionX, 9));
            param1.WriteInt(param1.Shift(this.positionY, 3));
            param1.WriteInt(param1.Shift(this.targetId, 30));
            param1.WriteShort(-13935);
            param1.WriteInt(param1.Shift(this.userId, 13));
        }
    }
}
