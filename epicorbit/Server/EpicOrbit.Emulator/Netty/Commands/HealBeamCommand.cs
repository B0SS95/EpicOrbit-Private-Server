using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class HealBeamCommand : ICommand {

        public short ID { get; set; } = 22016;
        public int sourcePositionY = 0;
        public int sourcePositionX = 0;
        public string sourceId = "";
        public int targetId = 0;

        public HealBeamCommand(string param1 = "", int param2 = 0, int param3 = 0, int param4 = 0) {
            this.sourceId = param1;
            this.sourcePositionX = param2;
            this.sourcePositionY = param3;
            this.targetId = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.sourcePositionY = param1.ReadInt();
            this.sourcePositionY = param1.Shift(this.sourcePositionY, 3);
            this.sourcePositionX = param1.ReadInt();
            this.sourcePositionX = param1.Shift(this.sourcePositionX, 9);
            this.sourceId = param1.ReadUTF();
            this.targetId = param1.ReadInt();
            this.targetId = param1.Shift(this.targetId, 15);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.sourcePositionY, 29));
            param1.WriteInt(param1.Shift(this.sourcePositionX, 23));
            param1.WriteUTF(this.sourceId);
            param1.WriteInt(param1.Shift(this.targetId, 17));
        }
    }
}
