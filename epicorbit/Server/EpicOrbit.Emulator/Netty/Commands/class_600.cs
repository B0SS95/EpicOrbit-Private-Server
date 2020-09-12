using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_600 : ICommand {

        // CRAFTING

        public short ID { get; set; } = 30937;
        public List<class_717> recipes;

        public class_600(List<class_717> param1 = null) {
            if (param1 == null) {
                this.recipes = new List<class_717>();
            } else {
                this.recipes = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.recipes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_717;
                tmp_0.Read(param1, lookup);
                this.recipes.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(27580);
            param1.WriteInt(this.recipes.Count);
            foreach (var tmp_0 in this.recipes) {
                tmp_0.Write(param1);
            }
        }
    }
}
