using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1037 : ICommand {

        public short ID { get; set; } = 20840;
        public int quality = 0;
        public string endian = "";
        public int var_3447 = 0;
        public int typeId = 0;
        public int name_170 = 0;
        public string var_1747 = "";
        public int var_2577 = 0;
        public int var_3124 = 0;
        public int length = 0;

        public class_1037(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, string param6 = "", int param7 = 0, string param8 = "", int param9 = 0) {
            this.name_170 = param1;
            this.typeId = param2;
            this.var_3124 = param3;
            this.var_3447 = param4;
            this.var_2577 = param5;
            this.var_1747 = param6;
            this.quality = param7;
            this.endian = param8;
            this.length = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.quality = param1.ReadInt();
            this.quality = param1.Shift(this.quality, 9);
            this.endian = param1.ReadUTF();
            this.var_3447 = param1.ReadInt();
            this.var_3447 = param1.Shift(this.var_3447, 24);
            this.typeId = param1.ReadInt();
            this.typeId = param1.Shift(this.typeId, 1);
            this.name_170 = param1.ReadInt();
            this.name_170 = param1.Shift(this.name_170, 16);
            this.var_1747 = param1.ReadUTF();
            this.var_2577 = param1.ReadInt();
            this.var_2577 = param1.Shift(this.var_2577, 2);
            this.var_3124 = param1.ReadInt();
            this.var_3124 = param1.Shift(this.var_3124, 21);
            this.length = param1.ReadInt();
            this.length = param1.Shift(this.length, 16);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.quality, 23));
            param1.WriteUTF(this.endian);
            param1.WriteInt(param1.Shift(this.var_3447, 8));
            param1.WriteInt(param1.Shift(this.typeId, 31));
            param1.WriteInt(param1.Shift(this.name_170, 16));
            param1.WriteUTF(this.var_1747);
            param1.WriteInt(param1.Shift(this.var_2577, 30));
            param1.WriteInt(param1.Shift(this.var_3124, 11));
            param1.WriteInt(param1.Shift(this.length, 16));
            param1.WriteShort(5987);
        }
    }
}
