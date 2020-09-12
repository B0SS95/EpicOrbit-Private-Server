using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_678 : class_677, ICommand {

        public override short ID { get; set; } = 404;
        public float var_2824 = 0;

        public class_678(string param1 = "", float param2 = 0)
         : base(param1) {
            this.var_2824 = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_2824 = param1.ReadFloat();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteFloat(this.var_2824);
            param1.WriteShort(-7762);
        }
    }
}
