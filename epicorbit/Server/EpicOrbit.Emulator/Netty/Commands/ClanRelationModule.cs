using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClanRelationModule : ICommand {

        public const short NONE = 0;
        public const short NON_AGGRESSION_PACT = 2;
        public const short AT_WAR = 3;
        public const short ALLIED = 1;
        public short ID { get; set; } = 19499;
        public short type = 0;

        public ClanRelationModule(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
        }
    }
}
