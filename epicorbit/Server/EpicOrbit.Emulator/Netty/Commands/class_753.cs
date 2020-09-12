using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_753 : class_752, ICommand {

        public override short ID { get; set; } = 30325;
        public int var_1197 = 0;

        public class_753(string param1 = "", int param2 = 0, int param3 = 0, int param4 = 0)
         : base(param1, param4, param3) {
            this.var_1197 = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_1197 = param1.ReadInt();
            this.var_1197 = param1.Shift(this.var_1197, 14);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_1197, 18));
        }
    }
}
