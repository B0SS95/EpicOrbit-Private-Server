using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AlignmentModule : ICommand {

        public const short SOUTH = 4;
        public const short CENTER = 8;
        public const short EAST = 2;
        public const short WEST = 6;
        public const short SOUTH_EAST = 3;
        public const short NORTH = 0;
        public const short NORT_EAST = 1;
        public const short NORTH_WEST = 7;
        public const short SOUTH_WEST = 5;
        public short ID { get; set; } = 26382;
        public short alignmentValue = 0;

        public AlignmentModule(short param1 = 0) {
            this.alignmentValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.alignmentValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.alignmentValue);
        }
    }
}
