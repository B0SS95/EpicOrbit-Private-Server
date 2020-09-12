using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_692 : class_691, ICommand {

        public override short ID { get; set; } = 5516;
        public int var_2179 = 0;

        public class_692(int param1 = 0) {
            this.var_2179 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_2179 = param1.ReadInt();
            this.var_2179 = param1.Shift(this.var_2179, 31);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.var_2179, 1));
            param1.WriteShort(10770);
        }
    }
}
