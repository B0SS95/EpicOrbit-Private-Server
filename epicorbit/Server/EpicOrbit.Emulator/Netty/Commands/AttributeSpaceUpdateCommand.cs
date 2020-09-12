using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeSpaceUpdateCommand : ICommand {

        public const short ROCKETS = 2;
        public const short BATTERIES = 1;
        public const short CARGO = 0;
        public short ID { get; set; } = 26216;
        public int spaceLeft = 0;
        public short spaceType = 0;

        public AttributeSpaceUpdateCommand(short param1 = 0, int param2 = 0) {
            this.spaceType = param1;
            this.spaceLeft = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.spaceLeft = param1.ReadInt();
            this.spaceLeft = param1.Shift(this.spaceLeft, 12);
            this.spaceType = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.spaceLeft, 20));
            param1.WriteShort(this.spaceType);
        }
    }
}
