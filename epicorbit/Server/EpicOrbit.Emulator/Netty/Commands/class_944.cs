using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_944 : ICommand {

        public short ID { get; set; } = 25472;
        public double var_4120 = 0;
        public List<class_827> reward;
        public string name_176 = "";

        public class_944(string param1 = "", double param2 = 0, List<class_827> param3 = null) {
            this.name_176 = param1;
            this.var_4120 = param2;
            if (param3 == null) {
                this.reward = new List<class_827>();
            } else {
                this.reward = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4120 = param1.ReadDouble();
            this.reward.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_827;
                tmp_0.Read(param1, lookup);
                this.reward.Add(tmp_0);
            }
            param1.ReadShort();
            this.name_176 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.var_4120);
            param1.WriteInt(this.reward.Count);
            foreach (var tmp_0 in this.reward) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(6196);
            param1.WriteUTF(this.name_176);
        }
    }
}
