using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class WindowIDModule : ICommand {

        public const short CHAT = 14;
        public const short MENU_QUICK = 25;
        public const short ASSIST_WINDOW = 21;
        public const short REFINEMENT = 17;
        public const short VIDEO_WINDOW = 20;
        public const short HELP = 6;
        public const short SPACEBALL = 10;
        public const short ACHIEVEMENTS = 18;
        public const short PET = 19;
        public const short BOOSTER = 9;
        public const short INVASION = 11;
        public const short LOG = 3;
        public const short MENU_MAIN = 24;
        public const short SHIP_INFO = 1;
        public const short TDM = 13;
        public const short CTB = 12;
        public const short ADVERTISEMENT = 23;
        public const short MINIMAP = 4;
        public const short GROUP = 16;
        public const short SETTINGS = 5;
        public const short MINIMAP_WINDOW = 22;
        public const short USER_INFO = 0;
        public const short TRADE = 7;
        public const short QUEST = 2;
        public const short MENU_TOP = 26;
        public const short SPACEMAP = 8;
        public const short CLI = 15;
        public short ID { get; set; } = 7795;
        public short idValue = 0;

        public WindowIDModule(short param1 = 0) {
            this.idValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.idValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.idValue);
        }
    }
}
