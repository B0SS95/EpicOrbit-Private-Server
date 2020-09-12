using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_720 : class_698, ICommand {

        public override short ID { get; set; } = 23011;
        public FactionModule faction;

        public class_720(FactionModule param1 = null) {
            if (param1 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param1;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            param1.ReadShort();
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(-32738);
            param1.WriteShort(-15019);
            this.faction.Write(param1);
        }
    }
}
