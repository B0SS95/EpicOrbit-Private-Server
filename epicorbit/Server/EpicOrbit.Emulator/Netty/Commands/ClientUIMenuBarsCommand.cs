using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUIMenuBarsCommand : ICommand {

        public short ID { get; set; } = 3763;
        public List<ClientUIMenuBarModule> var_4173;

        public ClientUIMenuBarsCommand(List<ClientUIMenuBarModule> param1 = null) {
            if (param1 == null) {
                this.var_4173 = new List<ClientUIMenuBarModule>();
            } else {
                this.var_4173 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4173.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUIMenuBarModule;
                tmp_0.Read(param1, lookup);
                this.var_4173.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.var_4173.Count);
            foreach (var tmp_0 in this.var_4173) {
                tmp_0.Write(param1);
            }
        }
    }
}
