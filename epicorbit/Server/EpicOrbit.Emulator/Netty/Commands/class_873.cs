using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_873 : ICommand {

        public short ID { get; set; } = 16365;
        public class_1010 var_3783;
        public class_1066 var_713;
        public ContactsModule var_5139;

        public class_873(ContactsModule param1 = null, class_1066 param2 = null, class_1010 param3 = null) {
            if (param1 == null) {
                this.var_5139 = new ContactsModule();
            } else {
                this.var_5139 = param1;
            }
            if (param2 == null) {
                this.var_713 = new class_1066();
            } else {
                this.var_713 = param2;
            }
            if (param3 == null) {
                this.var_3783 = new class_1010();
            } else {
                this.var_3783 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_3783 = lookup.Lookup(param1) as class_1010;
            this.var_3783.Read(param1, lookup);
            param1.ReadShort();
            this.var_713 = lookup.Lookup(param1) as class_1066;
            this.var_713.Read(param1, lookup);
            param1.ReadShort();
            this.var_5139 = lookup.Lookup(param1) as ContactsModule;
            this.var_5139.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_3783.Write(param1);
            param1.WriteShort(-16073);
            this.var_713.Write(param1);
            param1.WriteShort(-2335);
            this.var_5139.Write(param1);
        }
    }
}
