using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1089 : class_635, ICommand {

        public override short ID { get; set; } = 23917;
        public ClientUITooltipModule message;

        public class_1089(ClientUITooltipModule param1 = null) {
            if (param1 == null) {
                this.message = new ClientUITooltipModule();
            } else {
                this.message = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.message = lookup.Lookup(param1) as ClientUITooltipModule;
            this.message.Read(param1, lookup);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            this.message.Write(param1);
        }
    }
}
