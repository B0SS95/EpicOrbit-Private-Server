using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_570 : class_569, ICommand {

        public override short ID { get; set; } = 15966;
        public int var_3340 = 0;
        public int var_1390 = 0;
        public int var_2097 = 0;

        public class_570(int param1 = 0, double param2 = 0, double param3 = 0, int param4 = 0, double param5 = 0, int param6 = 0)
         : base(param3, param2, param5) {
            this.var_3340 = param6;
            this.var_1390 = param4;
            this.var_2097 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_3340 = param1.ReadInt();
            this.var_3340 = param1.Shift(this.var_3340, 18);
            this.var_1390 = param1.ReadInt();
            this.var_1390 = param1.Shift(this.var_1390, 14);
            this.var_2097 = param1.ReadInt();
            this.var_2097 = param1.Shift(this.var_2097, 12);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_3340, 14));
            param1.WriteInt(param1.Shift(this.var_1390, 18));
            param1.WriteInt(param1.Shift(this.var_2097, 20));
        }
    }
}
