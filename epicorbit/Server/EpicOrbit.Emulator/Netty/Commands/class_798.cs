using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_798 : ICommand {

        public short ID { get; set; } = 5105;
        public string var_2469 = "";
        public string var_3315 = "";
        public string var_2510 = "";

        public class_798(string param1 = "", string param2 = "", string param3 = "") {
            this.var_2469 = param1;
            this.var_2510 = param2;
            this.var_3315 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2469 = param1.ReadUTF();
            param1.ReadShort();
            this.var_3315 = param1.ReadUTF();
            param1.ReadShort();
            this.var_2510 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.var_2469);
            param1.WriteShort(-6858);
            param1.WriteUTF(this.var_3315);
            param1.WriteShort(-1872);
            param1.WriteUTF(this.var_2510);
        }
    }
}
