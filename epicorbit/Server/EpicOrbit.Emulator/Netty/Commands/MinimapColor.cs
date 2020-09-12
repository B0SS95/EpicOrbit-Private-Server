using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MinimapColor : ICommand {

        public const short ENEMY = 2;
        public const short DEFAULT = 0;
        public const short BLUE = 1;
        public short ID { get; set; } = 28346;
        public short typeValue = 0;

        public MinimapColor(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.typeValue);
            param1.WriteShort(-7779);
        }
    }
}
