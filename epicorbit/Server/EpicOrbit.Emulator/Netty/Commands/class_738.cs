using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_738 : class_505, ICommand {

        public override short ID { get; set; } = 25305;
        public class_944 name_132;
        public class_674 name_63;
        public class_1009 rank;

        public class_738(class_1009 param1 = null, class_944 param2 = null, class_674 param3 = null) {
            if (param1 == null) {
                this.rank = new class_1009();
            } else {
                this.rank = param1;
            }
            if (param3 == null) {
                this.name_63 = new class_674();
            } else {
                this.name_63 = param3;
            }
            if (param2 == null) {
                this.name_132 = new class_944();
            } else {
                this.name_132 = param2;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            param1.ReadShort();
            this.name_132 = lookup.Lookup(param1) as class_944;
            this.name_132.Read(param1, lookup);
            this.name_63 = lookup.Lookup(param1) as class_674;
            this.name_63.Read(param1, lookup);
            this.rank = lookup.Lookup(param1) as class_1009;
            this.rank.Read(param1, lookup);
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteShort(656);
            this.name_132.Write(param1);
            this.name_63.Write(param1);
            this.rank.Write(param1);
            param1.WriteShort(-4869);
        }
    }
}
