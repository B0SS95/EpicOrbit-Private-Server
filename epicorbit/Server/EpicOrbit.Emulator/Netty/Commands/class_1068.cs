using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1068 : ICommand {

        public const short const_3427 = 0;
        public short ID { get; set; } = 15631;
        public float interval = 0;
        public short type = 0;
        public int count = 0;
        public class_954 var_4005;
        public int var_19 = 0;
        public int var_3470 = 0;
        public string uid = "";
        public bool inverse = false;
        public float scale = 0;

        public class_1068(string param1 = "", int param2 = 0, int param3 = 0, short param4 = 0, int param5 = 0, float param6 = 0, float param7 = 0, bool param8 = false, class_954 param9 = null) {
            this.uid = param1;
            this.var_3470 = param2;
            this.var_19 = param3;
            this.type = param4;
            this.count = param5;
            this.interval = param6;
            this.scale = param7;
            this.inverse = param8;
            if (param9 == null) {
                this.var_4005 = new class_954();
            } else {
                this.var_4005 = param9;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.interval = param1.ReadFloat();
            this.type = param1.ReadShort();
            this.count = param1.ReadInt();
            this.count = param1.Shift(this.count, 8);
            this.var_4005 = lookup.Lookup(param1) as class_954;
            this.var_4005.Read(param1, lookup);
            this.var_19 = param1.ReadInt();
            this.var_19 = param1.Shift(this.var_19, 9);
            this.var_3470 = param1.ReadInt();
            this.var_3470 = param1.Shift(this.var_3470, 1);
            this.uid = param1.ReadUTF();
            this.inverse = param1.ReadBoolean();
            this.scale = param1.ReadFloat();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteFloat(this.interval);
            param1.WriteShort(this.type);
            param1.WriteInt(param1.Shift(this.count, 24));
            this.var_4005.Write(param1);
            param1.WriteInt(param1.Shift(this.var_19, 23));
            param1.WriteInt(param1.Shift(this.var_3470, 31));
            param1.WriteUTF(this.uid);
            param1.WriteBoolean(this.inverse);
            param1.WriteFloat(this.scale);
        }
    }
}
