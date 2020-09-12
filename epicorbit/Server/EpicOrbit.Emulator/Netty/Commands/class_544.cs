using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_544 : class_543, ICommand {

        public override short ID { get; set; } = 18569;
        public ClanRelationModule name_103;

        public class_544(ClanRelationModule param1 = null) {
            if (param1 == null) {
                this.name_103 = new ClanRelationModule();
            } else {
                this.name_103 = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.name_103 = lookup.Lookup(param1) as ClanRelationModule;
            this.name_103.Read(param1, lookup);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            this.name_103.Write(param1);
            param1.WriteShort(19507);
        }
    }
}
