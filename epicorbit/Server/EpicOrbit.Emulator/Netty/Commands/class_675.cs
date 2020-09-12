using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_675 : ICommand {

        public short ID { get; set; } = 22383;
        public bool var_1097 = false;
        public int var_4506 = 0;
        public int var_2753 = 0;

        public class_675(int param1 = 0, int param2 = 0, bool param3 = false) {
            this.var_2753 = param1;
            this.var_4506 = param2;
            this.var_1097 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1097 = param1.ReadBoolean();
            this.var_4506 = param1.ReadInt();
            this.var_4506 = param1.Shift(this.var_4506, 14);
            this.var_2753 = param1.ReadInt();
            this.var_2753 = param1.Shift(this.var_2753, 6);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_1097);
            param1.WriteInt(param1.Shift(this.var_4506, 18));
            param1.WriteInt(param1.Shift(this.var_2753, 26));
        }
    }
}
