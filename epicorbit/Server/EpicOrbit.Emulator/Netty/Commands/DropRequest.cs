using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class DropRequest : ICommand {

        public const short SMARTBOMB = 8;
        public const short FIREWORK_1 = 1;
        public const short MINE_EMP = 5;
        public const short MINE_DD = 7;
        public const short const_1549 = 0;
        public const short FIREWORK_2 = 2;
        public const short MINE = 4;
        public const short MINE_SAB = 6;
        public const short FIREWORK_3 = 3;
        public short ID { get; set; } = 13824;
        public short toDrop = 0;

        public DropRequest(short param1 = 0) {
            this.toDrop = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.toDrop = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-16861);
            param1.WriteShort(this.toDrop);
        }
    }
}
