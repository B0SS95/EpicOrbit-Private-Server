using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class PartnerTypeModule : ICommand {

        public const short DARKORBIT = 1;
        public const short FROST = 4;
        public const short const_1712 = 3;
        public const short ANTEC = 0;
        public const short RAZER = 2;
        public short ID { get; set; } = 20644;
        public short typeValue = 0;

        public PartnerTypeModule(short param1 = 0) {
            this.typeValue = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.typeValue = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.typeValue);
        }
    }
}
