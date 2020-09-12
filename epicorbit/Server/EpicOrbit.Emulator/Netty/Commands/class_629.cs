using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_629 : ICommand {

        // CTBSetHomeZonesCommand
        // MapEventEMPActivationCommand

        public short ID { get; set; } = 3810;
        public int y = 0;
        public int x = 0;
        public int var_4506 = 0;

        public class_629(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_4506 = param1;
            this.x = param2;
            this.y = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 26);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 11);
            this.var_4506 = param1.ReadInt();
            this.var_4506 = param1.Shift(this.var_4506, 3);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.y, 6));
            param1.WriteInt(param1.Shift(this.x, 21));
            param1.WriteInt(param1.Shift(this.var_4506, 29));
        }
    }
}
