using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestTypeModule : ICommand {

        public const short CHALLENGE = 4;
        public const short MISSION = 2;
        public const short STARTER = 1;
        public const short UNDEFINED = 0;
        public const short DAILY = 3;
        public const short const_384 = 6; // weekly?
        public const short EVENT = 5;
        public short ID { get; set; } = 24979;
        public short type = 0;

        public QuestTypeModule(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(2038);
            param1.WriteShort(this.type);
        }
    }
}
