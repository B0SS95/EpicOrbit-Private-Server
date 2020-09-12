using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestAcceptabilityStatusModule : ICommand {

        public const short COMPLETED = 3;
        public const short NOT_ACCEPTABLE = 0;
        public const short RUNNING = 2;
        public const short NOT_STARTED = 1;
        public const short DISABLED = 4;
        public short ID { get; set; } = 20978;
        public short type = 0;

        public QuestAcceptabilityStatusModule(short param1 = 0) {
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
