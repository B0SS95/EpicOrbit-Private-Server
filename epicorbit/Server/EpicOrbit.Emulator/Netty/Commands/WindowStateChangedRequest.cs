using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class WindowStateChangedRequest : ICommand {

        public short ID { get; set; } = 16656;
        public int x = 0;
        public int y = 0;
        public bool maximized = false;
        public string itemId = "";
        public int height = 0;
        public int width = 0;

        public WindowStateChangedRequest(string param1 = "", int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, bool param6 = false) {
            this.itemId = param1;
            this.x = param2;
            this.y = param3;
            this.width = param4;
            this.height = param5;
            this.maximized = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 17);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 10);
            param1.ReadShort();
            this.maximized = param1.ReadBoolean();
            this.itemId = param1.ReadUTF();
            this.height = param1.ReadInt();
            this.height = param1.Shift(this.height, 21);
            this.width = param1.ReadInt();
            this.width = param1.Shift(this.width, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.x, 15));
            param1.WriteInt(param1.Shift(this.y, 22));
            param1.WriteShort(-14220);
            param1.WriteBoolean(this.maximized);
            param1.WriteUTF(this.itemId);
            param1.WriteInt(param1.Shift(this.height, 11));
            param1.WriteInt(param1.Shift(this.width, 20));
        }
    }
}
