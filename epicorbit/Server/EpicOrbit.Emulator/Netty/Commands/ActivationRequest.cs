using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ActivationRequest : ICommand {

        public const short INSTASHIELD = 4;
        public const short CPU_JUMP = 9;
        public const short ADVANCED_JUMP_CPU = 10;
        public const short CPU_CLOAK = 5;
        public const short HELLSTORM = 11;
        public const short ROBOT_CANCEL = 1;
        public const short FIREWORK_IGNITE = 12;
        public const short INSTANTREPAIR = 6;
        public const short SMARTBOMB = 3;
        public const short PRANK = 8;
        public const short EMP = 2;
        public const short WIZARD = 7;
        public const short ROBOT = 0;
        public short ID { get; set; } = 29260;
        public short activationTypeValue = 0;

        public ActivationRequest(short param1 = 0) {
            this.activationTypeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.activationTypeValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.activationTypeValue);
            param1.WriteShort(28818);
        }
    }
}
