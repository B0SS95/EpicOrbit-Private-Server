using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowActionModule : ICommand {

        public const short HIDE_MENU = 7;
        public const short HIDE_WINDOW = 1;
        public const short MINIMIZE_WINDOW = 2;
        public const short HIDE_WINDOW_FLASH = 5;
        public const short SHOW_MENU = 6;
        public const short MAXIMIZE_WINDOW = 3;
        public const short SHOW_WINDOW = 0;
        public const short SHOW_WINDOW_FLASH = 4;
        public short ID { get; set; } = 17002;
        public int maxFlashes = 0;
        public short windowAction = 0;
        public WindowIDModule windowID;
        public bool showArrow = false;

        public UIWindowActionModule(short param1 = 0, WindowIDModule param2 = null, int param3 = 0, bool param4 = false) {
            this.windowAction = param1;
            if (param2 == null) {
                this.windowID = new WindowIDModule();
            } else {
                this.windowID = param2;
            }
            this.maxFlashes = param3;
            this.showArrow = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.maxFlashes = param1.ReadInt();
            this.maxFlashes = param1.Shift(this.maxFlashes, 26);
            this.windowAction = param1.ReadShort();
            this.windowID = lookup.Lookup(param1) as WindowIDModule;
            this.windowID.Read(param1, lookup);
            this.showArrow = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.maxFlashes, 6));
            param1.WriteShort(this.windowAction);
            this.windowID.Write(param1);
            param1.WriteBoolean(this.showArrow);
        }
    }
}
