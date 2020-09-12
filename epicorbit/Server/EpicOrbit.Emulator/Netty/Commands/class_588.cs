using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_588 : class_503, ICommand {

        public override short ID { get; set; } = 29467;
        public int var_3975 = 0;
        public int var_2297 = 0;
        public int hp = 0;
        public int shield = 0;
        public int name_113 = 0;
        public int var_718 = 0;

        public class_588(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0) {
            this.hp = param1;
            this.var_718 = param2;
            this.shield = param5;
            this.name_113 = param6;
            this.var_2297 = param3;
            this.var_3975 = param4;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_3975 = param1.ReadInt();
            this.var_3975 = param1.Shift(this.var_3975, 12);
            this.var_2297 = param1.ReadInt();
            this.var_2297 = param1.Shift(this.var_2297, 3);
            this.hp = param1.ReadInt();
            this.hp = param1.Shift(this.hp, 16);
            this.shield = param1.ReadInt();
            this.shield = param1.Shift(this.shield, 19);
            this.name_113 = param1.ReadInt();
            this.name_113 = param1.Shift(this.name_113, 6);
            this.var_718 = param1.ReadInt();
            this.var_718 = param1.Shift(this.var_718, 5);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_3975, 20));
            param1.WriteInt(param1.Shift(this.var_2297, 29));
            param1.WriteInt(param1.Shift(this.hp, 16));
            param1.WriteInt(param1.Shift(this.shield, 13));
            param1.WriteInt(param1.Shift(this.name_113, 26));
            param1.WriteInt(param1.Shift(this.var_718, 27));
        }
    }
}
