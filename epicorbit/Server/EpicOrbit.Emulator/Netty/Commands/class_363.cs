using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_363 : ICommand {

        public virtual short ID { get; set; } = 1689;
        public string key = "";

        public class_363(string param1 = "") {
            this.key = param1;
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.key = param1.ReadUTF();
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteShort(-10530);
            param1.WriteShort(-12223);
            param1.WriteUTF(this.key);
        }
    }
}
