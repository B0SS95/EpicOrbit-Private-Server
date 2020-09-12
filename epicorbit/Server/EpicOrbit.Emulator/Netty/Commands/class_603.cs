using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_603 : class_540, ICommand {

        public override short ID { get; set; } = 26004;
        public bool value = false;

        public class_603(string param1 = "", bool param2 = false)
         : base(param1) {
            this.value = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            param1.ReadShort();
            this.value = param1.ReadBoolean();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(19027);
            param1.WriteShort(-3943);
            param1.WriteBoolean(this.value);
        }
    }
}
