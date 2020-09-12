using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_176 : ICommand {

        public const short INACTIVE = 0;
        public const short const_1554 = 4;
        public const short DRONE = 2;
        public const short const_1143 = 1;
        public const short const_1705 = 6;
        public const short PET = 3;
        public const short const_1546 = 5;
        public short ID { get; set; } = 6332;
        public int var_1632 = 0;
        public int var_5010 = 0;
        public int var_2772 = 0;
        public int var_3982 = 0;
        public int var_826 = 0;
        public int var_5265 = 0;
        public int var_2110 = 0;
        public int var_1991 = 0;
        public short type = 0;

        public class_176(short param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0) {
            this.type = param1;
            this.var_2110 = param2;
            this.var_2772 = param3;
            this.var_5265 = param4;
            this.var_5010 = param5;
            this.var_1632 = param6;
            this.var_1991 = param7;
            this.var_826 = param8;
            this.var_3982 = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1632 = param1.ReadInt();
            this.var_1632 = param1.Shift(this.var_1632, 28);
            this.var_5010 = param1.ReadInt();
            this.var_5010 = param1.Shift(this.var_5010, 18);
            this.var_2772 = param1.ReadInt();
            this.var_2772 = param1.Shift(this.var_2772, 16);
            this.var_3982 = param1.ReadInt();
            this.var_3982 = param1.Shift(this.var_3982, 30);
            this.var_826 = param1.ReadInt();
            this.var_826 = param1.Shift(this.var_826, 3);
            param1.ReadShort();
            this.var_5265 = param1.ReadInt();
            this.var_5265 = param1.Shift(this.var_5265, 8);
            this.var_2110 = param1.ReadInt();
            this.var_2110 = param1.Shift(this.var_2110, 7);
            this.var_1991 = param1.ReadInt();
            this.var_1991 = param1.Shift(this.var_1991, 20);
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_1632, 4));
            param1.WriteInt(param1.Shift(this.var_5010, 14));
            param1.WriteInt(param1.Shift(this.var_2772, 16));
            param1.WriteInt(param1.Shift(this.var_3982, 2));
            param1.WriteInt(param1.Shift(this.var_826, 29));
            param1.WriteShort(-14439);
            param1.WriteInt(param1.Shift(this.var_5265, 24));
            param1.WriteInt(param1.Shift(this.var_2110, 25));
            param1.WriteInt(param1.Shift(this.var_1991, 12));
            param1.WriteShort(this.type);
        }
    }
}
