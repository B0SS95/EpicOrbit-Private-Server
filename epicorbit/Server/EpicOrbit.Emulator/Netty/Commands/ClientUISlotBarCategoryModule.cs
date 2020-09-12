using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarCategoryModule : ICommand {

        public short ID { get; set; } = 26440;
        public List<ClientUISlotBarCategoryItemModule> var_796;
        public string var_4499 = "";

        public ClientUISlotBarCategoryModule(string param1 = "", List<ClientUISlotBarCategoryItemModule> param2 = null) {
            this.var_4499 = param1;
            if (param2 == null) {
                this.var_796 = new List<ClientUISlotBarCategoryItemModule>();
            } else {
                this.var_796 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_796.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUISlotBarCategoryItemModule;
                tmp_0.Read(param1, lookup);
                this.var_796.Add(tmp_0);
            }
            this.var_4499 = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_796.Count);
            foreach (var tmp_0 in this.var_796) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.var_4499);
            param1.WriteShort(5684);
        }
    }
}
