using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_541 : class_540, ICommand {

        public override short ID { get; set; } = 26630;
        public string value = "";

        public class_541(string param1 = "", string param2 = "")
         : base(param1) {
            this.value = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.value = param1.ReadUTF();
            param1.ReadShort();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteUTF(this.value);
            param1.WriteShort(-13416);
            param1.WriteShort(25976);
        }
    }
}
