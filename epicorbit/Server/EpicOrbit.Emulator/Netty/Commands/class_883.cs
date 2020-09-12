using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_883 : ICommand {

        // Mines????

        public const short DAMAGE = 0;
        public const short const_158 = 2;
        public const short SHIELD = 1;
        public short ID { get; set; } = 9938;
        public short type = 0;
        public int x = 0;
        public int y = 0;
        public int var_4422 = 0;
        public int radius = 0;

        public class_883(short param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0) {
            this.type = param1;
            this.x = param2;
            this.y = param3;
            this.var_4422 = param4;
            this.radius = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = param1.ReadShort();
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 29);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 29);
            this.var_4422 = param1.ReadInt();
            this.var_4422 = param1.Shift(this.var_4422, 17);
            param1.ReadShort();
            this.radius = param1.ReadInt();
            this.radius = param1.Shift(this.radius, 1);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.type);
            param1.WriteInt(param1.Shift(this.x, 3));
            param1.WriteInt(param1.Shift(this.y, 3));
            param1.WriteInt(param1.Shift(this.var_4422, 15));
            param1.WriteShort(21501);
            param1.WriteInt(param1.Shift(this.radius, 31));
        }
    }
}
