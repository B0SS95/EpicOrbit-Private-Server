using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ActivationStatedRequest : ICommand {

        public const short ROCKET_LAUNCHER_CPU = 1;
        public const short CPU_AIM = 2;
        public const short AUTOROCKET = 0;
        public short ID { get; set; } = 23708;
        public short activationType = 0;
        public bool active = false;

        public ActivationStatedRequest(short param1 = 0, bool param2 = false) {
            this.activationType = param1;
            this.active = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.activationType = param1.ReadShort();
            this.active = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.activationType);
            param1.WriteBoolean(this.active);
            param1.WriteShort(-13911);
        }
    }
}
