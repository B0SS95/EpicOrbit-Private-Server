using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowSettingModule : ICommand {

        public short ID { get; set; } = 28882;
        public WindowIDModule windowID;
        public bool maximized = false;
        public int x = 0;
        public int y = 0;

        public UIWindowSettingModule(WindowIDModule param1 = null, int param2 = 0, int param3 = 0, bool param4 = false) {
            if (param1 == null) {
                this.windowID = new WindowIDModule();
            } else {
                this.windowID = param1;
            }
            this.x = param2;
            this.y = param3;
            this.maximized = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.windowID = lookup.Lookup(param1) as WindowIDModule;
            this.windowID.Read(param1, lookup);
            this.maximized = param1.ReadBoolean();
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 19);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 30);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.windowID.Write(param1);
            param1.WriteBoolean(this.maximized);
            param1.WriteInt(param1.Shift(this.x, 13));
            param1.WriteInt(param1.Shift(this.y, 2));
        }
    }
}
