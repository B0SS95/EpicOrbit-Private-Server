using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_968 : ICommand {

        public short ID { get; set; } = 24034;
        public class_607 var_2563;

        public class_968(class_607 param1 = null) {
            if (param1 == null) {
                this.var_2563 = new class_607();
            } else {
                this.var_2563 = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_2563 = lookup.Lookup(param1) as class_607;
            this.var_2563.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(1313);
            this.var_2563.Write(param1);
            param1.WriteShort(21392);
        }
    }
}
