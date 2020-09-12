using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DestructionTypeModule : ICommand {

        public const short PLAYER = 0;
        public const short NPC = 1;
        public const short MINE = 3;
        public const short RADITATION = 2;
        public const short BATTLESTATION = 5;
        public const short MISC = 4;
        public short ID { get; set; } = 3296;
        public short cause = 0;

        public DestructionTypeModule(short param1 = 0) {
            this.cause = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.cause = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-22167);
            param1.WriteShort(this.cause);
        }
    }
}
