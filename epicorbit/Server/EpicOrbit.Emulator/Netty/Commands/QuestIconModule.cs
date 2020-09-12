using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestIconModule : ICommand {

        public const short const_2241 = 2;
        public const short const_611 = 0;
        public const short const_1711 = 11;
        public const short PVP = 3;
        public const short COLLECT = 1;
        public const short const_877 = 6;
        public const short const_2957 = 8;
        public const short const_1754 = 5;
        public const short const_3106 = 7;
        public const short const_3299 = 9;
        public const short TIME = 4;
        public const short const_1477 = 10;
        public short ID { get; set; } = 4503;
        public short icon = 0;

        public QuestIconModule(short param1 = 0) {
            this.icon = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.icon = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.icon);
            param1.WriteShort(-16119);
        }
    }
}
