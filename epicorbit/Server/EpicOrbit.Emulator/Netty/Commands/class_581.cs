using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_581 : ICommand {

        // SectorControlMatchStateInfoCommand - nope!!

        public short ID { get; set; } = 15477;
        public List<class_677> var_4161;
        public List<class_677> var_1929;
        public List<class_900> var_1867;

        public class_581(List<class_900> param1 = null, List<class_677> param2 = null, List<class_677> param3 = null) {
            if (param1 == null) {
                this.var_1867 = new List<class_900>();
            } else {
                this.var_1867 = param1;
            }
            if (param2 == null) {
                this.var_4161 = new List<class_677>();
            } else {
                this.var_4161 = param2;
            }
            if (param3 == null) {
                this.var_1929 = new List<class_677>();
            } else {
                this.var_1929 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4161.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_677;
                tmp_0.Read(param1, lookup);
                this.var_4161.Add(tmp_0);
            }
            this.var_1929.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_677;
                tmp_0.Read(param1, lookup);
                this.var_1929.Add(tmp_0);
            }
            this.var_1867.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_900;
                tmp_0.Read(param1, lookup);
                this.var_1867.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_4161.Count);
            foreach (var tmp_0 in this.var_4161) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.var_1929.Count);
            foreach (var tmp_0 in this.var_1929) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.var_1867.Count);
            foreach (var tmp_0 in this.var_1867) {
                tmp_0.Write(param1);
            }
        }
    }
}
