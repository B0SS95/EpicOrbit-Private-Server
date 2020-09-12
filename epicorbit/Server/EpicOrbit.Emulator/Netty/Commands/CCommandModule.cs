using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CCommandModule : ICommand {

        public short ID { get; set; } = 22648;
        public bool var_1797 = false;
        public string toolTip = "";
        public List<class_615> args;
        public string name = "";

        public CCommandModule(string param1 = "", string param2 = "", bool param3 = false, List<class_615> param4 = null) {
            this.name = param1;
            this.toolTip = param2;
            this.var_1797 = param3;
            if (param4 == null) {
                this.args = new List<class_615>();
            } else {
                this.args = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_1797 = param1.ReadBoolean();
            param1.ReadShort();
            this.toolTip = param1.ReadUTF();
            param1.ReadShort();
            this.args.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_615;
                tmp_0.Read(param1, lookup);
                this.args.Add(tmp_0);
            }
            this.name = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_1797);
            param1.WriteShort(-9067);
            param1.WriteUTF(this.toolTip);
            param1.WriteShort(-18407);
            param1.WriteInt(this.args.Count);
            foreach (var tmp_0 in this.args) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.name);
        }
    }
}
