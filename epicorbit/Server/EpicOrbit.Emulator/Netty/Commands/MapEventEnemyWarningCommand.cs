using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapEventEnemyWarningCommand : ICommand {

        public short ID { get; set; } = 27876;
        public int enemyCount = 0;

        public MapEventEnemyWarningCommand(int param1 = 0) {
            this.enemyCount = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.enemyCount = param1.ReadInt();
            this.enemyCount = param1.Shift(this.enemyCount, 1);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.enemyCount, 31));
            param1.WriteShort(12935);
        }
    }
}
