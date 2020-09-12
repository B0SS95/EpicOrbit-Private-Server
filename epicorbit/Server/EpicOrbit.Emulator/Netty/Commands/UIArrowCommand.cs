using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIArrowCommand : ICommand {

        public short ID { get; set; } = 28451;
        public int x = 0;
        public int y = 0;
        public bool show = false;

        public UIArrowCommand(int param1 = 0, int param2 = 0, bool param3 = false) {
            this.x = param1;
            this.y = param2;
            this.show = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 24);
            param1.ReadShort();
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 15);
            this.show = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.x, 8));
            param1.WriteShort(-2727);
            param1.WriteInt(param1.Shift(this.y, 17));
            param1.WriteBoolean(this.show);
        }
    }
}
