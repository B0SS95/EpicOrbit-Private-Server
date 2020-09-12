using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUITooltipTextFormatModule : ICommand {

        public const short LOCALIZED = 5;
        public const short const_296 = 7;
        public const short const_3135 = 1;
        public const short const_2837 = 2;
        public const short PLAIN = 0;
        public const short const_3010 = 8;
        public const short const_2434 = 6;
        public const short const_2526 = 4;
        public const short const_1373 = 3;
        public short ID { get; set; } = 24194;
        public short name_87 = 0;

        public ClientUITooltipTextFormatModule(short param1 = 0) {
            this.name_87 = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_87 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.name_87);
        }
    }
}
