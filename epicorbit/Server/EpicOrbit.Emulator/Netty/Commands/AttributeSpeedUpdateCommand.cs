using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeSpeedUpdateCommand : ICommand {

        public short ID { get; set; } = 21270;
        public int stated = 0;
        public int actual = 0;

        public AttributeSpeedUpdateCommand(int param1 = 0, int param2 = 0) {
            this.stated = param1;
            this.actual = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.stated = param1.ReadInt();
            this.stated = param1.Shift(this.stated, 10);
            this.actual = param1.ReadInt();
            this.actual = param1.Shift(this.actual, 10);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.stated, 22));
            param1.WriteInt(param1.Shift(this.actual, 22));
        }
    }
}
