using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_858 : ICommand {

        public short ID { get; set; } = 27996;
        public int var_2753 = 0;
        public int var_4506 = 0;
        public List<int> var_24;

        public class_858(int param1 = 0, int param2 = 0, List<int> param3 = null) {
            this.var_2753 = param1;
            this.var_4506 = param2;
            if (param3 == null) {
                this.var_24 = new List<int>();
            } else {
                this.var_24 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2753 = param1.ReadInt();
            this.var_2753 = param1.Shift(this.var_2753, 11);
            this.var_4506 = param1.ReadInt();
            this.var_4506 = param1.Shift(this.var_4506, 11);
            this.var_24.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 2);
                this.var_24.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.var_2753, 21));
            param1.WriteInt(param1.Shift(this.var_4506, 21));
            param1.WriteInt(this.var_24.Count);
            foreach (var tmp_0 in this.var_24) {
                param1.WriteInt(param1.Shift(tmp_0, 30));
            }
        }
    }
}
