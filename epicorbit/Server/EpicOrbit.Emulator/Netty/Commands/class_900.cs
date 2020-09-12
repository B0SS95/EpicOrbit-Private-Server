using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_900 : ICommand {

        public short ID { get; set; } = 31311;
        public string var_2531 = "";
        public float value = 0;

        public class_900(string param1 = "", float param2 = 0) {
            this.var_2531 = param1;
            this.value = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2531 = param1.ReadUTF();
            this.value = param1.ReadFloat();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.var_2531);
            param1.WriteFloat(this.value);
        }
    }
}
