using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_664 : ICommand {

        public short ID { get; set; } = 26828;
        public List<class_980> var_2821;
        public class_528 var_1190;
        public class_627 var_1721;

        public class_664(class_528 param1 = null, class_627 param2 = null, List<class_980> param3 = null) {
            if (param1 == null) {
                this.var_1190 = new class_528();
            } else {
                this.var_1190 = param1;
            }
            if (param2 == null) {
                this.var_1721 = new class_627();
            } else {
                this.var_1721 = param2;
            }
            if (param3 == null) {
                this.var_2821 = new List<class_980>();
            } else {
                this.var_2821 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2821.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_980;
                tmp_0.Read(param1, lookup);
                this.var_2821.Add(tmp_0);
            }
            this.var_1190 = lookup.Lookup(param1) as class_528;
            this.var_1190.Read(param1, lookup);
            param1.ReadShort();
            this.var_1721 = lookup.Lookup(param1) as class_627;
            this.var_1721.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_2821.Count);
            foreach (var tmp_0 in this.var_2821) {
                tmp_0.Write(param1);
            }
            this.var_1190.Write(param1);
            param1.WriteShort(-9117);
            this.var_1721.Write(param1);
        }
    }
}
