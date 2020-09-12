using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1041 : ICommand {

        public const short const_1162 = 2;
        public const short const_1419 = 1;
        public const short NONE = 0;
        public short ID { get; set; } = 26347;
        public int name_175 = 0;
        public FactionModule name_80;
        public short var_2275 = 0;
        public string var_3566 = "";
        public List<FactionModule> name_160;
        public double name_4 = 0;

        public class_1041(string param1 = "", FactionModule param2 = null, List<FactionModule> param3 = null, int param4 = 0, double param5 = 0, short param6 = 0) {
            this.var_3566 = param1;
            if (param2 == null) {
                this.name_80 = new FactionModule();
            } else {
                this.name_80 = param2;
            }
            if (param3 == null) {
                this.name_160 = new List<FactionModule>();
            } else {
                this.name_160 = param3;
            }
            this.name_175 = param4;
            this.name_4 = param5;
            this.var_2275 = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_175 = param1.ReadInt();
            this.name_175 = param1.Shift(this.name_175, 30);
            this.name_80 = lookup.Lookup(param1) as FactionModule;
            this.name_80.Read(param1, lookup);
            this.var_2275 = param1.ReadShort();
            this.var_3566 = param1.ReadUTF();
            param1.ReadShort();
            this.name_160.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as FactionModule;
                tmp_0.Read(param1, lookup);
                this.name_160.Add(tmp_0);
            }
            param1.ReadShort();
            this.name_4 = param1.ReadDouble();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.name_175, 2));
            this.name_80.Write(param1);
            param1.WriteShort(this.var_2275);
            param1.WriteUTF(this.var_3566);
            param1.WriteShort(-3427);
            param1.WriteInt(this.name_160.Count);
            foreach (var tmp_0 in this.name_160) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-4314);
            param1.WriteDouble(this.name_4);
        }
    }
}
