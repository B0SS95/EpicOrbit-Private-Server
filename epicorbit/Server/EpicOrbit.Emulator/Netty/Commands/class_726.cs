using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_726 : ICommand {

        public short ID { get; set; } = 16384;
        public int id = 0;
        public int type = 0;
        public int var_2374 = 0;
        public int var_514 = 0;
        public int var_2931 = 0;

        public class_726(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.id = param1;
            this.type = param2;
            this.var_2931 = param3;
            this.var_514 = param4;
            this.var_2374 = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 16);
            param1.ReadShort();
            this.type = param1.ReadInt();
            this.type = param1.Shift(this.type, 24);
            this.var_2374 = param1.ReadInt();
            this.var_2374 = param1.Shift(this.var_2374, 26);
            this.var_514 = param1.ReadInt();
            this.var_514 = param1.Shift(this.var_514, 2);
            this.var_2931 = param1.ReadInt();
            this.var_2931 = param1.Shift(this.var_2931, 6);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.id, 16));
            param1.WriteShort(-13813);
            param1.WriteInt(param1.Shift(this.type, 8));
            param1.WriteInt(param1.Shift(this.var_2374, 6));
            param1.WriteInt(param1.Shift(this.var_514, 30));
            param1.WriteInt(param1.Shift(this.var_2931, 26));
        }
    }
}
