using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetGearActivationRequest : ICommand {

        public short ID { get; set; } = 15665;
        public PetGearTypeModule gearTypeToActivate;
        public short optParam = 0;

        public PetGearActivationRequest(PetGearTypeModule param1 = null, short param2 = 0) {
            if (param1 == null) {
                this.gearTypeToActivate = new PetGearTypeModule();
            } else {
                this.gearTypeToActivate = param1;
            }
            this.optParam = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.gearTypeToActivate = lookup.Lookup(param1) as PetGearTypeModule;
            this.gearTypeToActivate.Read(param1, lookup);
            param1.ReadShort();
            this.optParam = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.gearTypeToActivate.Write(param1);
            param1.WriteShort(12937);
            param1.WriteShort(this.optParam);
            param1.WriteShort(-28133);
        }
    }
}
