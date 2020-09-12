using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_771 : ICommand {

        public short ID { get; set; } = 5467;
        public class_775 var_2793;
        public bool name_83 = false;

        public class_771(class_775 param1 = null, bool param2 = false) {
            if (param1 == null) {
                this.var_2793 = new class_775();
            } else {
                this.var_2793 = param1;
            }
            this.name_83 = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2793 = lookup.Lookup(param1) as class_775;
            this.var_2793.Read(param1, lookup);
            param1.ReadShort();
            param1.ReadShort();
            this.name_83 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_2793.Write(param1);
            param1.WriteShort(21526);
            param1.WriteShort(21282);
            param1.WriteBoolean(this.name_83);
        }
    }
}
