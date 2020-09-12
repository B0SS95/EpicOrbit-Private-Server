using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class KillScreenOptionTypeModule : ICommand {

        public const short const_2373 = 7;
        public const short const_10 = 10;
        public const short BASIC_FULL_REPAIR = 6;
        public const short AT_SECTOR_CONTROL_SPAWNPOINT = 4;
        public const short AT_DEATHLOCATION_REPAIR = 3;
        public const short EXIT_SECTOR_CONTROL = 5;
        public const short AT_JUMPGATE_REPAIR = 2;
        public const short const_1772 = 11;
        public const short FREE_PHOENIX = 0;
        public const short const_709 = 8;
        public const short const_3355 = 9;
        public const short BASIC_REPAIR = 1;
        public short ID { get; set; } = 30915;
        public short repairTypeValue = 0;

        public KillScreenOptionTypeModule(short param1 = 0) {
            this.repairTypeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.repairTypeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-30616);
            param1.WriteShort(-30097);
            param1.WriteShort(this.repairTypeValue);
        }
    }
}
