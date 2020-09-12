using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_869 : ICommand {

        public short ID { get; set; } = 1318;
        public bool var_5066 = false;
        public int var_2327 = 0;

        public class_869(int param1 = 0, bool param2 = false) {
            this.var_2327 = param1;
            this.var_5066 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_5066 = param1.ReadBoolean();
            this.var_2327 = param1.ReadInt();
            this.var_2327 = param1.Shift(this.var_2327, 14);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(6965);
            param1.WriteBoolean(this.var_5066);
            param1.WriteInt(param1.Shift(this.var_2327, 18));
            param1.WriteShort(-19050);
        }
    }
}
