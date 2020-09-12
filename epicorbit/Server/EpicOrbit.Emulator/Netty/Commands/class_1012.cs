using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1012 : ICommand {

        public short ID { get; set; } = 9373;
        public bool active = false;
        public PetGearTypeModule var_372;

        public class_1012(PetGearTypeModule param1 = null, bool param2 = false) {
            if (param1 == null) {
                this.var_372 = new PetGearTypeModule();
            } else {
                this.var_372 = param1;
            }
            this.active = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.active = param1.ReadBoolean();
            this.var_372 = lookup.Lookup(param1) as PetGearTypeModule;
            this.var_372.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.active);
            this.var_372.Write(param1);
        }
    }
}
