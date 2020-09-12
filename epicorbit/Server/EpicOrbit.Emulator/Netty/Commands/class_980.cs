using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_980 : ICommand {

        public const short const_2406 = 2;
        public const short const_415 = 0;
        public const short const_2741 = 1;
        public short ID { get; set; } = 23836;
        public class_633 var_3505;
        public short var_78 = 0;
        public class_633 var_4694;

        public class_980(short param1 = 0, class_633 param2 = null, class_633 param3 = null) {
            this.var_78 = param1;
            if (param2 == null) {
                this.var_4694 = new class_633();
            } else {
                this.var_4694 = param2;
            }
            if (param3 == null) {
                this.var_3505 = new class_633();
            } else {
                this.var_3505 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3505 = lookup.Lookup(param1) as class_633;
            this.var_3505.Read(param1, lookup);
            this.var_78 = param1.ReadShort();
            this.var_4694 = lookup.Lookup(param1) as class_633;
            this.var_4694.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_3505.Write(param1);
            param1.WriteShort(this.var_78);
            this.var_4694.Write(param1);
        }
    }
}
