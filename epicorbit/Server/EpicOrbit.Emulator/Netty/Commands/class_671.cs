using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_671 : ICommand {

        public short ID { get; set; } = 15514;
        public double playerScore = 0;

        public class_671(double param1 = 0) {
            this.playerScore = param1;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playerScore = param1.ReadDouble();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteDouble(this.playerScore);
            param1.WriteShort(1995);
        }
    }
}
