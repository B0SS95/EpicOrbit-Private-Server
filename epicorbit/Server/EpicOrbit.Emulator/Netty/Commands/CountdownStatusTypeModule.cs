using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CountdownStatusTypeModule : ICommand {

        public const short HALLOWEEN_2012 = 0;
        public short ID { get; set; } = 15372;
        public short type = 0;

        public CountdownStatusTypeModule(short param1 = 0) {
            this.type = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.type = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-27579);
            param1.WriteShort(this.type);
        }
    }
}
