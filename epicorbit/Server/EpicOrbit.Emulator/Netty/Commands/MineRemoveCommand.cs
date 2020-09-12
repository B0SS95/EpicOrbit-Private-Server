using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MineRemoveCommand : ICommand {

        public short ID { get; set; } = 21622;
        public string hash = "";

        public MineRemoveCommand(string param1 = "") {
            this.hash = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hash = param1.ReadUTF();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.hash);
            param1.WriteShort(27209);
            param1.WriteShort(31808);
        }
    }
}
