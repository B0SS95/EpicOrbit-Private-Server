using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_480 : ICommand {

        // CTBSetHomeZonesCommand
        // MapEventEMPActivationCommand

        public short ID { get; set; } = 22467;
        public int var_1197 = 0;
        public int x = 0;
        public int y = 0;

        public class_480(int param1 = 0, int param2 = 0, int param3 = 0) {
            this.var_1197 = param1;
            this.x = param2;
            this.y = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_1197 = param1.ReadInt();
            this.var_1197 = param1.Shift(this.var_1197, 4);
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 30);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 12);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-18177);
            param1.WriteInt(param1.Shift(this.var_1197, 28));
            param1.WriteInt(param1.Shift(this.x, 2));
            param1.WriteInt(param1.Shift(this.y, 20));
        }
    }
}
