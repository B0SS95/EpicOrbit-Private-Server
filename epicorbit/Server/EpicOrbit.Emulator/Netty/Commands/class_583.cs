using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_583 : ICommand {

        public short ID { get; set; } = 27209;
        public bool var_2134 = false;

        public class_583(bool param1 = false) {
            this.var_2134 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2134 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_2134);
        }
    }
}
