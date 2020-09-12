using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1015 : class_543, ICommand {

        public override short ID { get; set; } = 6096;
        public bool name_86 = false;

        public class_1015(bool param1 = false) {
            this.name_86 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.name_86 = param1.ReadBoolean();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteBoolean(this.name_86);
        }
    }
}
