using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_568 : ICommand {

        public short ID { get; set; } = 11391;
        public short var_3485 = 0;

        public class_568(short param1 = 0) {
            this.var_3485 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3485 = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_3485);
            param1.WriteShort(30556);
            param1.WriteShort(12427);
        }
    }
}
