using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LabItemModule : ICommand {

        public const short ROCKETS = 1;
        public const short LASER = 0;
        public const short SHIELD = 3;
        public const short DRIVING = 2;
        public short ID { get; set; } = 12716;
        public short itemValue = 0;

        public LabItemModule(short param1 = 0) {
            this.itemValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemValue = param1.ReadShort();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.itemValue);
            param1.WriteShort(-6139);
        }
    }
}
