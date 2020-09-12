using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIMinimapMarkerCommand : ICommand {

        public short ID { get; set; } = 2231;
        public bool playSound = false;
        public int maxPing = 0;
        public int x = 0;
        public int id = 0;
        public int y = 0;

        public UIMinimapMarkerCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, bool param5 = false) {
            this.id = param1;
            this.x = param2;
            this.y = param3;
            this.maxPing = param4;
            this.playSound = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.playSound = param1.ReadBoolean();
            this.maxPing = param1.ReadInt();
            this.maxPing = param1.Shift(this.maxPing, 31);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 17);
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 29);
            param1.ReadShort();
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 6);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(20366);
            param1.WriteBoolean(this.playSound);
            param1.WriteInt(param1.Shift(this.maxPing, 1));
            param1.WriteInt(param1.Shift(this.x, 15));
            param1.WriteInt(param1.Shift(this.id, 3));
            param1.WriteShort(-14641);
            param1.WriteInt(param1.Shift(this.y, 26));
        }
    }
}
