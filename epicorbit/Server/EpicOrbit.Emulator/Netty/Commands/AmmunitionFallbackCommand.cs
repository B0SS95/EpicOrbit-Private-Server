using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AmmunitionFallbackCommand : ICommand {

        public const short ROCKET = 1;
        public const short MINES = 2;
        public const short LASER = 0;
        public short ID { get; set; } = 12730;
        public short ammunitionType = 0;
        public int typeIdOld = 0;
        public int typeIdNew = 0;

        public AmmunitionFallbackCommand(short param1 = 0, int param2 = 0, int param3 = 0) {
            this.ammunitionType = param1;
            this.typeIdOld = param2;
            this.typeIdNew = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.ammunitionType = param1.ReadShort();
            param1.ReadShort();
            param1.ReadShort();
            this.typeIdOld = param1.ReadInt();
            this.typeIdOld = param1.Shift(this.typeIdOld, 6);
            this.typeIdNew = param1.ReadInt();
            this.typeIdNew = param1.Shift(this.typeIdNew, 14);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.ammunitionType);
            param1.WriteShort(2448);
            param1.WriteShort(23458);
            param1.WriteInt(param1.Shift(this.typeIdOld, 26));
            param1.WriteInt(param1.Shift(this.typeIdNew, 18));
        }
    }
}
