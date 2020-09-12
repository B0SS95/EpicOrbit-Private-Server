using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_787 : ICommand {

        public short ID { get; set; } = 15882;
        public string hash = "";
        public bool name_41 = false;

        public class_787(string param1 = "", bool param2 = false) {
            this.hash = param1;
            this.name_41 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hash = param1.ReadUTF();
            param1.ReadShort();
            this.name_41 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.hash);
            param1.WriteShort(-22217);
            param1.WriteBoolean(this.name_41);
        }
    }
}
