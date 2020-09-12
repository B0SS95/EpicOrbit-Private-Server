using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeLevelUpUpdateCommand : ICommand {

        public short ID { get; set; } = 8965;
        public int epToNextLevel = 0;
        public int level = 0;

        public AttributeLevelUpUpdateCommand(int param1 = 0, int param2 = 0) {
            this.level = param1;
            this.epToNextLevel = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.epToNextLevel = param1.ReadInt();
            this.epToNextLevel = param1.Shift(this.epToNextLevel, 12);
            param1.ReadShort();
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 9);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.epToNextLevel, 20));
            param1.WriteShort(26889);
            param1.WriteInt(param1.Shift(this.level, 23));
        }
    }
}
