using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1074 : ICommand {

        public short ID { get; set; } = 16172;
        public string var_2233 = "";
        public string value = "";

        public class_1074(string param1 = "", string param2 = "") {
            this.var_2233 = param1;
            this.value = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2233 = param1.ReadUTF();
            this.value = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.var_2233);
            param1.WriteUTF(this.value);
            param1.WriteShort(-29756);
        }
    }
}
