using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PetRequest : ICommand {

        public const short REPAIR_DESTROYED_PET = 4;
        public const short LAUNCH = 0;
        public const short TOGGLE_ACTIVATION = 2;
        public const short HOTKEY_REPAIR_SHIP = 5;
        public const short DEACTIVATE = 1;
        public const short HOTKEY_GUARD_MODE = 3;
        public short ID { get; set; } = 7623;
        public short petRequestType = 0;

        public PetRequest(short param1 = 0) {
            this.petRequestType = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.petRequestType = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(2067);
            param1.WriteShort(10958);
            param1.WriteShort(this.petRequestType);
        }
    }
}
