using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_747 : class_677, ICommand {

        public override short ID { get; set; } = 7460;
        public int var_4374 = 0;

        public class_747(string param1 = "", int param2 = 0)
         : base(param1) {
            this.var_4374 = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.var_4374 = param1.ReadInt();
            this.var_4374 = param1.Shift(this.var_4374, 14);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(25313);
            param1.WriteInt(param1.Shift(this.var_4374, 18));
        }
    }
}
