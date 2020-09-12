using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowSelectRequest : ICommand {

        public short ID { get; set; } = 3688;
        public string itemId = "";

        public UIWindowSelectRequest(string param1 = "") {
            this.itemId = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.itemId);
        }
    }
}
