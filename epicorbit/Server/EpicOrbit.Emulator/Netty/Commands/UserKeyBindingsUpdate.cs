using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UserKeyBindingsUpdate : ICommand {

        public short ID { get; set; } = 17210;
        public List<UserKeyBindingsModule> var_3733;
        public bool remove = false;

        public UserKeyBindingsUpdate(List<UserKeyBindingsModule> param1 = null, bool param2 = false) {
            if (param1 == null) {
                this.var_3733 = new List<UserKeyBindingsModule>();
            } else {
                this.var_3733 = param1;
            }
            this.remove = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3733.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as UserKeyBindingsModule;
                tmp_0.Read(param1, lookup);
                this.var_3733.Add(tmp_0);
            }
            this.remove = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_3733.Count);
            foreach (var tmp_0 in this.var_3733) {
                tmp_0.Write(param1);
            }
            param1.WriteBoolean(this.remove);
            param1.WriteShort(-21442);
        }
    }
}
