using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_589 : ICommand {

        public short ID { get; set; } = 19137;
        public string value = "";
        public string key = "";

        public class_589(string param1 = "", string param2 = "") {
            this.key = param1;
            this.value = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.value = param1.ReadUTF();
            this.key = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(12500);
            param1.WriteUTF(this.value);
            param1.WriteUTF(this.key);
            param1.WriteShort(4441);
        }
    }
}
