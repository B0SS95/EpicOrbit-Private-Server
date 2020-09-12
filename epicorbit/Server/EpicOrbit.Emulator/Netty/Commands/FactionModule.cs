using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class FactionModule : class_503, ICommand {

        public const short VRU = 3;
        public const short EIC = 2;
        public const short NONE = 0;
        public const short MMO = 1;
        public override short ID { get; set; } = 11940;
        public short faction = 0;

        public FactionModule(short param1 = 0) {
            this.faction = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.faction = param1.ReadShort();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(this.faction);
            param1.WriteShort(-27351);
        }
    }
}
