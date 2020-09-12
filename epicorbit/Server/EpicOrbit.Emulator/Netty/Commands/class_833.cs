using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_833 : ICommand {

        public short ID { get; set; } = 27807;
        public List<string> var_870;

        public class_833(List<string> param1 = null) {
            if (param1 == null) {
                this.var_870 = new List<String>();
            } else {
                this.var_870 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_870.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.ReadUTF();
                this.var_870.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(13338);
            param1.WriteInt(this.var_870.Count);
            foreach (var tmp_0 in this.var_870) {
                param1.WriteUTF(tmp_0);
            }
        }
    }
}
