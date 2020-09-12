using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_851 : ICommand {

        public short ID { get; set; } = 25658;
        public string var_909 = "";

        public class_851(string param1 = "") {
            this.var_909 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_909 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.var_909);
        }
    }
}
