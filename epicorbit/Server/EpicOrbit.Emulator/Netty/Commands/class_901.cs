using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_901 : ICommand {

        public short ID { get; set; } = 31401;
        public List<int> var_618;

        public class_901(List<int> param1 = null) {
            if (param1 == null) {
                this.var_618 = new List<int>();
            } else {
                this.var_618 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_618.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 18);
                this.var_618.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_618.Count);
            foreach (var tmp_0 in this.var_618) {
                param1.WriteInt(param1.Shift(tmp_0, 14));
            }
        }
    }
}
