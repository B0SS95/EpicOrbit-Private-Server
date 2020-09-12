using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_621 : ICommand {

        public short ID { get; set; } = 12469;
        public bool var_2279 = false;
        public List<LootModule> var_345;
        public string name_15 = "";
        public bool closeable = false;
        public string key = "";
        public AlignmentModule alignment;
        public string price = "";
        public int var_2824 = 0;
        public string link = "";

        public class_621(string param1 = "", string param2 = "", string param3 = "", string param4 = "", List<LootModule> param5 = null, int param6 = 0, AlignmentModule param7 = null, bool param8 = false, bool param9 = false) {
            this.name_15 = param1;
            this.key = param2;
            this.link = param3;
            this.price = param4;
            if (param5 == null) {
                this.var_345 = new List<LootModule>();
            } else {
                this.var_345 = param5;
            }
            this.var_2824 = param6;
            if (param7 == null) {
                this.alignment = new AlignmentModule();
            } else {
                this.alignment = param7;
            }
            this.closeable = param8;
            this.var_2279 = param9;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2279 = param1.ReadBoolean();
            this.var_345.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as LootModule;
                tmp_0.Read(param1, lookup);
                this.var_345.Add(tmp_0);
            }
            this.name_15 = param1.ReadUTF();
            this.closeable = param1.ReadBoolean();
            this.key = param1.ReadUTF();
            this.alignment = lookup.Lookup(param1) as AlignmentModule;
            this.alignment.Read(param1, lookup);
            this.price = param1.ReadUTF();
            this.var_2824 = param1.ReadInt();
            this.var_2824 = param1.Shift(this.var_2824, 8);
            this.link = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_2279);
            param1.WriteInt(this.var_345.Count);
            foreach (var tmp_0 in this.var_345) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.name_15);
            param1.WriteBoolean(this.closeable);
            param1.WriteUTF(this.key);
            this.alignment.Write(param1);
            param1.WriteUTF(this.price);
            param1.WriteInt(param1.Shift(this.var_2824, 24));
            param1.WriteUTF(this.link);
        }
    }
}
