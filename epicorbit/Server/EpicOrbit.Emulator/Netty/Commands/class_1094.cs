using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1094 : ICommand {

        public short ID { get; set; } = 12230;
        public bool var_2407 = false;
        public int var_2327 = 0;

        public class_1094(int param1 = 0, bool param2 = false) {
            this.var_2327 = param1;
            this.var_2407 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2407 = param1.ReadBoolean();
            this.var_2327 = param1.ReadInt();
            this.var_2327 = param1.Shift(this.var_2327, 9);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_2407);
            param1.WriteInt(param1.Shift(this.var_2327, 23));
            param1.WriteShort(-13611);
        }
    }
}
