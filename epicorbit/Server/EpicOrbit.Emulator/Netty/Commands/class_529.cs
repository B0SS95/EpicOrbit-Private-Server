using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_529 : ICommand {

        public short ID { get; set; } = 22918;
        public bool var_3368 = false;
        public bool var_658 = false;
        public bool var_4737 = false;
        public bool var_2218 = false;
        public bool var_1718 = false;
        public bool var_736 = false;

        public class_529(bool param1 = false, bool param2 = false, bool param3 = false, bool param4 = false, bool param5 = false, bool param6 = false) {
            this.var_4737 = param1;
            this.var_736 = param2;
            this.var_658 = param3;
            this.var_1718 = param4;
            this.var_3368 = param5;
            this.var_2218 = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3368 = param1.ReadBoolean();
            this.var_658 = param1.ReadBoolean();
            param1.ReadShort();
            this.var_4737 = param1.ReadBoolean();
            this.var_2218 = param1.ReadBoolean();
            this.var_1718 = param1.ReadBoolean();
            this.var_736 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_3368);
            param1.WriteBoolean(this.var_658);
            param1.WriteShort(30515);
            param1.WriteBoolean(this.var_4737);
            param1.WriteBoolean(this.var_2218);
            param1.WriteBoolean(this.var_1718);
            param1.WriteBoolean(this.var_736);
        }
    }
}
