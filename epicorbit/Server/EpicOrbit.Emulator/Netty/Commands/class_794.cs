using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_794 : ICommand {

        public short ID { get; set; } = 24093;
        public string language = "";
        public string endpointId = "";

        public class_794(string param1 = "", string param2 = "") {
            this.endpointId = param1;
            this.language = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.language = param1.ReadUTF();
            param1.ReadShort();
            this.endpointId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.language);
            param1.WriteShort(11626);
            param1.WriteUTF(this.endpointId);
        }
    }
}
