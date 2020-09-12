using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeShieldUpdateCommand : ICommand {

        public short ID { get; set; } = 28224;
        public int maxShield = 0;
        public int shield = 0;

        public AttributeShieldUpdateCommand(int param1 = 0, int param2 = 0) {
            this.shield = param1;
            this.maxShield = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.maxShield = param1.ReadInt();
            this.maxShield = param1.Shift(this.maxShield, 21);
            this.shield = param1.ReadInt();
            this.shield = param1.Shift(this.shield, 31);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(11741);
            param1.WriteShort(6249);
            param1.WriteInt(param1.Shift(this.maxShield, 11));
            param1.WriteInt(param1.Shift(this.shield, 1));
        }
    }
}
