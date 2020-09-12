using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1082 : ICommand {

        public short ID { get; set; } = 9382;
        public PetGearTypeModule var_438;
        public List<int> var_2682;

        public class_1082(PetGearTypeModule param1 = null, List<int> param2 = null) {
            if (param1 == null) {
                this.var_438 = new PetGearTypeModule();
            } else {
                this.var_438 = param1;
            }
            if (param2 == null) {
                this.var_2682 = new List<int>();
            } else {
                this.var_2682 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_438 = lookup.Lookup(param1) as PetGearTypeModule;
            this.var_438.Read(param1, lookup);
            this.var_2682.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 24);
                this.var_2682.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_438.Write(param1);
            param1.WriteInt(this.var_2682.Count);
            foreach (var tmp_0 in this.var_2682) {
                param1.WriteInt(param1.Shift(tmp_0, 8));
            }
            param1.WriteShort(32348);
        }
    }
}
