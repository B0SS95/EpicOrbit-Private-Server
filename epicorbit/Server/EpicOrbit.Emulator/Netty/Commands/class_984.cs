using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_984 : ICommand {

        public short ID { get; set; } = 24269;
        public int var_5175 = 0;
        public bool name_23 = false;

        public class_984(int param1 = 0, bool param2 = false) {
            this.var_5175 = param1;
            this.name_23 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5175 = param1.ReadInt();
            this.var_5175 = param1.Shift(this.var_5175, 3);
            param1.ReadShort();
            this.name_23 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_5175, 29));
            param1.WriteShort(-25120);
            param1.WriteBoolean(this.name_23);
        }
    }
}
