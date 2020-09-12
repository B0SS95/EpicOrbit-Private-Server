using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowBlockModeCommand : ICommand {

        public short ID { get; set; } = 6643;
        public WindowIDModule windowId;
        public bool blockWindow = false;

        public UIWindowBlockModeCommand(WindowIDModule param1 = null, bool param2 = false) {
            if (param1 == null) {
                this.windowId = new WindowIDModule();
            } else {
                this.windowId = param1;
            }
            this.blockWindow = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.windowId = lookup.Lookup(param1) as WindowIDModule;
            this.windowId.Read(param1, lookup);
            this.blockWindow = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-18871);
            this.windowId.Write(param1);
            param1.WriteBoolean(this.blockWindow);
        }
    }
}
