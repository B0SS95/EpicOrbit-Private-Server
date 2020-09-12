using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarsCommand : ICommand {

        public short ID { get; set; } = 8704;
        public List<ClientUISlotBarCategoryModule> categories;
        public string var_4420 = "";
        public List<ClientUISlotBarModule> slotBars;

        public ClientUISlotBarsCommand(List<ClientUISlotBarCategoryModule> param1 = null, string param2 = "", List<ClientUISlotBarModule> param3 = null) {
            if (param1 == null) {
                this.categories = new List<ClientUISlotBarCategoryModule>();
            } else {
                this.categories = param1;
            }
            this.var_4420 = param2;
            if (param3 == null) {
                this.slotBars = new List<ClientUISlotBarModule>();
            } else {
                this.slotBars = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.categories.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUISlotBarCategoryModule;
                tmp_0.Read(param1, lookup);
                this.categories.Add(tmp_0);
            }
            this.var_4420 = param1.ReadUTF();
            this.slotBars.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUISlotBarModule;
                tmp_0.Read(param1, lookup);
                this.slotBars.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.categories.Count);
            foreach (var tmp_0 in this.categories) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.var_4420);
            param1.WriteInt(this.slotBars.Count);
            foreach (var tmp_0 in this.slotBars) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(27548);
        }
    }
}
