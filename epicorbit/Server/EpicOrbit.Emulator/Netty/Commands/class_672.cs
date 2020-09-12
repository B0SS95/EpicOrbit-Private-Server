using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_672 : ICommand {

        public short ID { get; set; } = 1500;
        public string var_1887 = "";

        public class_672(string param1 = "") {
            this.var_1887 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1887 = param1.ReadUTF();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.var_1887);
            param1.WriteShort(4849);
            param1.WriteShort(15091);
        }
    }
}
