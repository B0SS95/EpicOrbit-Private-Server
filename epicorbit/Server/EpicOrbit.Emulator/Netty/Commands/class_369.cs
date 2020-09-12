using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_369 : ICommand {

        public short ID { get; set; } = 17053;
        public string var_4114 = "";

        public class_369(string param1 = "") {
            this.var_4114 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_4114 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-11574);
            param1.WriteUTF(this.var_4114);
        }
    }
}
