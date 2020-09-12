using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_528 : ICommand {

        // InfectedCommand

        public short ID { get; set; } = 29177;
        public int remainingTime = 0;
        public bool infected = false;

        public class_528(bool param1 = false, int param2 = 0) {
            this.infected = param1;
            this.remainingTime = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.remainingTime = param1.ReadInt();
            this.remainingTime = param1.Shift(this.remainingTime, 19);
            this.infected = param1.ReadBoolean();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.remainingTime, 13));
            param1.WriteBoolean(this.infected);
            param1.WriteShort(12528);
        }
    }
}
