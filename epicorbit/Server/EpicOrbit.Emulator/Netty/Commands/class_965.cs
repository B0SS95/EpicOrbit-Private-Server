using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_965 : ICommand {

        // PetLocatorGearInitializationCommand
        // PetGearSelectCommand

        public short ID { get; set; } = 25966;
        public List<int> var_2242;
        public PetGearTypeModule var_372;

        public class_965(PetGearTypeModule param1 = null, List<int> param2 = null) {
            if (param1 == null) {
                this.var_372 = new PetGearTypeModule();
            } else {
                this.var_372 = param1;
            }
            if (param2 == null) {
                this.var_2242 = new List<int>();
            } else {
                this.var_2242 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2242.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 23);
                this.var_2242.Add(tmp_0);
            }
            param1.ReadShort();
            this.var_372 = lookup.Lookup(param1) as PetGearTypeModule;
            this.var_372.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-12179);
            param1.WriteInt(this.var_2242.Count);
            foreach (var tmp_0 in this.var_2242) {
                param1.WriteInt(param1.Shift(tmp_0, 9));
            }
            param1.WriteShort(32091);
            this.var_372.Write(param1);
        }
    }
}
