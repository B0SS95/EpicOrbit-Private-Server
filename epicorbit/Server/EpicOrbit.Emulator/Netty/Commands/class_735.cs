using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_735 : ICommand {

        public const short const_728 = 0;
        public const short const_3445 = 1;
        public short ID { get; set; } = 6394;
        public short rewardType = 0;
        public int var_1372 = 0;
        public int var_2247 = 0;

        public class_735(int param1 = 0, int param2 = 0, short param3 = 0) {
            this.var_2247 = param1;
            this.var_1372 = param2;
            this.rewardType = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.rewardType = param1.ReadShort();
            param1.ReadShort();
            this.var_1372 = param1.ReadInt();
            this.var_1372 = param1.Shift(this.var_1372, 3);
            this.var_2247 = param1.ReadInt();
            this.var_2247 = param1.Shift(this.var_2247, 19);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.rewardType);
            param1.WriteShort(-18496);
            param1.WriteInt(param1.Shift(this.var_1372, 29));
            param1.WriteInt(param1.Shift(this.var_2247, 13));
        }
    }
}
