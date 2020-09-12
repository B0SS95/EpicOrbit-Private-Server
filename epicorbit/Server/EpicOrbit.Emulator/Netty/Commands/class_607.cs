using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_607 : ICommand {

        public virtual short ID { get; set; } = 7705;

        public class_607() {
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteShort(1524);
        }
    }
}
