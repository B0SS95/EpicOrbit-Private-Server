using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1049 : ICommand {

        public short ID { get; set; } = 3140;
        public bool name_23 = false;

        public class_1049(bool param1 = false) {
            this.name_23 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_23 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.name_23);
        }
    }
}
