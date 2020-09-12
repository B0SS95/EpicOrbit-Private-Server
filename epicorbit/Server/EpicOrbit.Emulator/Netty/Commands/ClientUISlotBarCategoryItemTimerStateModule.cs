using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUISlotBarCategoryItemTimerStateModule : ICommand {

        public const short ACTIVE = 1;
        public const short READY = 0;
        public const short const_2682 = 2;
        public short ID { get; set; } = 19195;
        public short state = 0;

        public ClientUISlotBarCategoryItemTimerStateModule(short param1 = 0) {
            this.state = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.state = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.state);
        }
    }
}
