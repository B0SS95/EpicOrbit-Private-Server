using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_706 : ICommand {

        public short ID { get; set; } = 21284;
        public int var_4820 = 0;
        public List<class_660> name_122;
        public int id = 0;
        public int var_1681 = 0;
        public class_828 var_1432;

        public class_706(int param1 = 0, int param2 = 0, int param3 = 0, List<class_660> param4 = null, class_828 param5 = null) {
            this.id = param1;
            this.var_1681 = param2;
            this.var_4820 = param3;
            if (param4 == null) {
                this.name_122 = new List<class_660>();
            } else {
                this.name_122 = param4;
            }
            if (param5 == null) {
                this.var_1432 = new class_828();
            } else {
                this.var_1432 = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4820 = param1.ReadInt();
            this.var_4820 = param1.Shift(this.var_4820, 8);
            this.name_122.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_660;
                tmp_0.Read(param1, lookup);
                this.name_122.Add(tmp_0);
            }
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 31);
            this.var_1681 = param1.ReadInt();
            this.var_1681 = param1.Shift(this.var_1681, 31);
            this.var_1432 = lookup.Lookup(param1) as class_828;
            this.var_1432.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_4820, 24));
            param1.WriteInt(this.name_122.Count);
            foreach (var tmp_0 in this.name_122) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.id, 1));
            param1.WriteInt(param1.Shift(this.var_1681, 1));
            this.var_1432.Write(param1);
        }
    }
}
