using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_618 : class_617, ICommand {

        public override short ID { get; set; } = 8636;
        public string titleKey = "";

        public class_618(string param1 = "") {
            this.titleKey = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.titleKey = param1.ReadUTF();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(2638);
            param1.WriteUTF(this.titleKey);
        }
    }
}
