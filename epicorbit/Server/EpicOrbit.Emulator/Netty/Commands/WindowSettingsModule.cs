using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class WindowSettingsModule : ICommand {

        public short ID { get; set; } = 12640;
        public bool hideAllWindows = false;
        public string barState = ""; // show number or show bar
        public int minimapScaleFactor = 0; // minimapSize

        public WindowSettingsModule(int param1 = 0, string param2 = "", bool param3 = false) {
            this.minimapScaleFactor = param1;
            this.barState = param2;
            this.hideAllWindows = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.hideAllWindows = param1.ReadBoolean();
            this.barState = param1.ReadUTF();
            this.minimapScaleFactor = param1.ReadInt();
            this.minimapScaleFactor = param1.Shift(this.minimapScaleFactor, 31);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(18726);
            param1.WriteBoolean(this.hideAllWindows);
            param1.WriteUTF(this.barState);
            param1.WriteInt(param1.Shift(this.minimapScaleFactor, 1));
            param1.WriteShort(-14154);
        }
    }
}
