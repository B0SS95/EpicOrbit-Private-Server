using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetInitializationCommand : ICommand {

        public short ID { get; set; } = 31421;
        public bool hasFuel = false;
        public bool hasPet = false;
        public bool petIsAlive = false;

        public PetInitializationCommand(bool param1 = false, bool param2 = false, bool param3 = false) {
            this.hasPet = param1;
            this.hasFuel = param2;
            this.petIsAlive = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hasFuel = param1.ReadBoolean();
            this.hasPet = param1.ReadBoolean();
            this.petIsAlive = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.hasFuel);
            param1.WriteBoolean(this.hasPet);
            param1.WriteBoolean(this.petIsAlive);
        }
    }
}
