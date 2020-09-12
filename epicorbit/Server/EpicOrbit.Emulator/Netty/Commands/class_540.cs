using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_540 : ICommand {

        public virtual short ID { get; set; } = 14184;
        public string name = "";

        public class_540(string param1 = "") {
            this.name = param1;
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            this.name = param1.ReadUTF();
            param1.ReadShort();
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteUTF(this.name);
            param1.WriteShort(21625);
        }
    }
}
