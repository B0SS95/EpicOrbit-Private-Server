using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1033 : ICommand {

        public short ID { get; set; } = 31583;
        public List<UIWindowSettingModule> var_3080;

        public class_1033(List<UIWindowSettingModule> param1 = null) {
            if (param1 == null) {
                this.var_3080 = new List<UIWindowSettingModule>();
            } else {
                this.var_3080 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3080.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as UIWindowSettingModule;
                tmp_0.Read(param1, lookup);
                this.var_3080.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_3080.Count);
            foreach (var tmp_0 in this.var_3080) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(30236);
        }
    }
}
