using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LogMessageCommand : ICommand {

        public const short const_976 = 1;
        public const short const_14 = 0;
        public const short SCREEN = 1;
        public const short const_2214 = 2;
        public const short LOG = 0;
        public short ID { get; set; } = 30786;
        public short style = 0;
        public MessageLocalizedWildcardCommand message;
        public short visibility = 0;

        public LogMessageCommand(short param1 = 0, short param2 = 0, MessageLocalizedWildcardCommand param3 = null) {
            this.visibility = param1;
            this.style = param2;
            if (param3 == null) {
                this.message = new MessageLocalizedWildcardCommand();
            } else {
                this.message = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.style = param1.ReadShort();
            param1.ReadShort();
            this.message = lookup.Lookup(param1) as MessageLocalizedWildcardCommand;
            this.message.Read(param1, lookup);
            this.visibility = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.style);
            param1.WriteShort(1458);
            this.message.Write(param1);
            param1.WriteShort(this.visibility);
        }
    }
}
