using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AddOreCommand : class_752, ICommand {

        public override short ID { get; set; } = 11042;
        public OreTypeModule var_5073;

        public AddOreCommand(string param1 = "", OreTypeModule param2 = null, int param3 = 0, int param4 = 0)
         : base(param1, param4, param3) {
            if (param2 == null) {
                this.var_5073 = new OreTypeModule();
            } else {
                this.var_5073 = param2;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_5073 = lookup.Lookup(param1) as OreTypeModule;
            this.var_5073.Read(param1, lookup);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            this.var_5073.Write(param1);
            param1.WriteShort(-15072);
        }
    }
}
