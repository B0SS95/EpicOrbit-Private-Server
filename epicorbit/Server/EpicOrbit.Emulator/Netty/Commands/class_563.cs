using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_563 : ICommand {

        public short ID { get; set; } = 26856;
        public int userId = 0;
        public List<class_1007> var_2410;

        public class_563(int param1 = 0, List<class_1007> param2 = null) {
            this.userId = param1;
            if (param2 == null) {
                this.var_2410 = new List<class_1007>();
            } else {
                this.var_2410 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 22);
            this.var_2410.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_1007;
                tmp_0.Read(param1, lookup);
                this.var_2410.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.userId, 10));
            param1.WriteInt(this.var_2410.Count);
            foreach (var tmp_0 in this.var_2410) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(26704);
        }
    }
}
