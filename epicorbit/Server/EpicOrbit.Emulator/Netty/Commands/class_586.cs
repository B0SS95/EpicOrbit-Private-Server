using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_586 : ICommand {

        public const short const_2416 = 2;
        public const short HP = 0;
        public const short LEVEL = 1;
        public short ID { get; set; } = 28567;
        public string value = "";
        public short key = 0;

        public class_586(short param1 = 0, string param2 = "") {
            this.key = param1;
            this.value = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.value = param1.ReadUTF();
            this.key = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.value);
            param1.WriteShort(this.key);
        }
    }
}
