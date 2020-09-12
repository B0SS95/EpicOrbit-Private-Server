using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_810 : ICommand {

        public short ID { get; set; } = 11775;
        public int var_2320 = 0;
        public List<class_503> updates;

        public class_810(int param1 = 0, List<class_503> param2 = null) {
            this.var_2320 = param1;
            if (param2 == null) {
                this.updates = new List<class_503>();
            } else {
                this.updates = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2320 = param1.ReadInt();
            this.var_2320 = param1.Shift(this.var_2320, 25);
            this.updates.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_503;
                tmp_0.Read(param1, lookup);
                this.updates.Add(tmp_0);
            }
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2320, 7));
            param1.WriteInt(this.updates.Count);
            foreach (var tmp_0 in this.updates) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(1442);
            param1.WriteShort(-22755);
        }
    }
}
