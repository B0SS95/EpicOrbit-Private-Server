using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_717 : ICommand {

        public short ID { get; set; } = 31295;
        public class_796 type;
        public class_547 status;
        public string var_909 = "";

        public class_717(string param1 = "", class_796 param2 = null, class_547 param3 = null) {
            this.var_909 = param1;
            if (param2 == null) {
                this.type = new class_796();
            } else {
                this.type = param2;
            }
            if (param3 == null) {
                this.status = new class_547();
            } else {
                this.status = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.type = lookup.Lookup(param1) as class_796;
            this.type.Read(param1, lookup);
            this.status = lookup.Lookup(param1) as class_547;
            this.status.Read(param1, lookup);
            this.var_909 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.type.Write(param1);
            this.status.Write(param1);
            param1.WriteUTF(this.var_909);
        }
    }
}
