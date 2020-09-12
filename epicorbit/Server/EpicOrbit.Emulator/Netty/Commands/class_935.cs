using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_935 : ICommand {

        public short ID { get; set; } = 16673;
        public class_954 var_4005;
        public class_518 var_248;
        public string itemId = "";
        public class_963 var_5008;

        public class_935(class_963 param1 = null, string param2 = "", class_518 param3 = null, class_954 param4 = null) {
            if (param1 == null) {
                this.var_5008 = new class_963();
            } else {
                this.var_5008 = param1;
            }
            this.itemId = param2;
            if (param3 == null) {
                this.var_248 = new class_518();
            } else {
                this.var_248 = param3;
            }
            if (param4 == null) {
                this.var_4005 = new class_954();
            } else {
                this.var_4005 = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4005 = lookup.Lookup(param1) as class_954;
            this.var_4005.Read(param1, lookup);
            this.var_248 = lookup.Lookup(param1) as class_518;
            this.var_248.Read(param1, lookup);
            this.itemId = param1.ReadUTF();
            param1.ReadShort();
            this.var_5008 = lookup.Lookup(param1) as class_963;
            this.var_5008.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_4005.Write(param1);
            this.var_248.Write(param1);
            param1.WriteUTF(this.itemId);
            param1.WriteShort(19273);
            this.var_5008.Write(param1);
        }
    }
}
