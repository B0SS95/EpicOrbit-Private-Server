using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarModule : ICommand {

        public short ID { get; set; } = 18922;
        public List<ClientUISlotBarItemModule> var_261;
        public string var_758 = "";
        public bool visible = false;
        public string var_3285 = "";
        public string slotBarId = "";

        public ClientUISlotBarModule(string param1 = "", List<ClientUISlotBarItemModule> param2 = null, string param3 = "", string param4 = "", bool param5 = false) {
            this.slotBarId = param1;
            if (param2 == null) {
                this.var_261 = new List<ClientUISlotBarItemModule>();
            } else {
                this.var_261 = param2;
            }
            this.var_3285 = param3;
            this.var_758 = param4;
            this.visible = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_261.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUISlotBarItemModule;
                tmp_0.Read(param1, lookup);
                this.var_261.Add(tmp_0);
            }
            this.var_758 = param1.ReadUTF();
            this.visible = param1.ReadBoolean();
            this.var_3285 = param1.ReadUTF();
            this.slotBarId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_261.Count);
            foreach (var tmp_0 in this.var_261) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.var_758);
            param1.WriteBoolean(this.visible);
            param1.WriteUTF(this.var_3285);
            param1.WriteUTF(this.slotBarId);
        }
    }
}
