using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AttributeHitpointUpdateCommand : ICommand {

        public short ID { get; set; } = 27024;
        public int hitpoints = 0;
        public int maxHitpoints = 0;
        public int nanohull = 0;
        public int maxNanohull = 0;

        public AttributeHitpointUpdateCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0) {
            this.hitpoints = param1;
            this.maxHitpoints = param2;
            this.nanohull = param3;
            this.maxNanohull = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.hitpoints = param1.ReadInt();
            this.hitpoints = param1.Shift(this.hitpoints, 15);
            this.maxHitpoints = param1.ReadInt();
            this.maxHitpoints = param1.Shift(this.maxHitpoints, 16);
            this.nanohull = param1.ReadInt();
            this.nanohull = param1.Shift(this.nanohull, 30);
            this.maxNanohull = param1.ReadInt();
            this.maxNanohull = param1.Shift(this.maxNanohull, 12);
            param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.hitpoints, 17));
            param1.WriteInt(param1.Shift(this.maxHitpoints, 16));
            param1.WriteInt(param1.Shift(this.nanohull, 2));
            param1.WriteInt(param1.Shift(this.maxNanohull, 20));
            param1.WriteShort(-10229);
            param1.WriteShort(885);
        }
    }
}
