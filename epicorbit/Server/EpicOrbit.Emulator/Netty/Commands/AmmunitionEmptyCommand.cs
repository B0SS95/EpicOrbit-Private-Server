using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionEmptyCommand : ICommand {

        public const short MINES = 2;
        public const short ROCKET = 1;
        public const short LASER = 0;
        public short ID { get; set; } = 23999;
        public short ammunitionType = 0;
        public bool alternativeAvailable = false;

        public AmmunitionEmptyCommand(short param1 = 0, bool param2 = false) {
            this.ammunitionType = param1;
            this.alternativeAvailable = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ammunitionType = param1.ReadShort();
            this.alternativeAvailable = param1.ReadBoolean();
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.ammunitionType);
            param1.WriteBoolean(this.alternativeAvailable);
            param1.WriteShort(-1668);
            param1.WriteShort(17260);
        }
    }
}
