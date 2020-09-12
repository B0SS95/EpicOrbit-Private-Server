using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_639 : ICommand {

        public short ID { get; set; } = 1162;
        public bool var_28 = false;
        public bool var_4933 = false;

        public class_639(bool param1 = false, bool param2 = false) {
            this.var_4933 = param1;
            this.var_28 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_28 = param1.ReadBoolean();
            this.var_4933 = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_28);
            param1.WriteBoolean(this.var_4933);
            param1.WriteShort(1678);
        }
    }
}
