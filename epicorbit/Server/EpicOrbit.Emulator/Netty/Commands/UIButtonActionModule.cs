using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIButtonActionModule : ICommand {

        public const short HIDE_BUTTON = 1;
        public const short HIDE_BUTTON_FLASH = 3;
        public const short DISABLE_MENU_BUTTON = 5;
        public const short ENABLE_MENU_BUTTON = 4;
        public const short SHOW_BUTTON_FLASH = 2;
        public const short SHOW_BUTTON = 0;
        public short ID { get; set; } = 26482;
        public int maxFlashes = 0;
        public short buttonAction = 0;
        public ButtonIDModule buttonID;
        public bool showArrow = false;

        public UIButtonActionModule(short param1 = 0, ButtonIDModule param2 = null, int param3 = 0, bool param4 = false) {
            this.buttonAction = param1;
            if (param2 == null) {
                this.buttonID = new ButtonIDModule();
            } else {
                this.buttonID = param2;
            }
            this.maxFlashes = param3;
            this.showArrow = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.maxFlashes = param1.ReadInt();
            this.maxFlashes = param1.Shift(this.maxFlashes, 21);
            this.buttonAction = param1.ReadShort();
            this.buttonID = lookup.Lookup(param1) as ButtonIDModule;
            this.buttonID.Read(param1, lookup);
            this.showArrow = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(17660);
            param1.WriteInt(param1.Shift(this.maxFlashes, 11));
            param1.WriteShort(this.buttonAction);
            this.buttonID.Write(param1);
            param1.WriteBoolean(this.showArrow);
        }
    }
}
