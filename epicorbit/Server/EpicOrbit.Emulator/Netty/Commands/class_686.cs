using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_686 : ICommand {

        public const short OPEN = 0;
        public const short const_2001 = 2;
        public const short const_3019 = 1;
        public short ID { get; set; } = 18403;
        public bool var_521 = false;
        public class_775 window;
        public class_954 var_4005;
        public string paymentLink = "";
        public bool var_3289 = false;
        public short visibility = 0;

        public class_686(class_775 param1 = null, bool param2 = false, short param3 = 0, bool param4 = false, class_954 param5 = null, string param6 = "") {
            if (param1 == null) {
                this.window = new class_775();
            } else {
                this.window = param1;
            }
            this.var_521 = param2;
            this.visibility = param3;
            this.var_3289 = param4;
            if (param5 == null) {
                this.var_4005 = new class_954();
            } else {
                this.var_4005 = param5;
            }
            this.paymentLink = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_521 = param1.ReadBoolean();
            param1.ReadShort();
            param1.ReadShort();
            this.window = lookup.Lookup(param1) as class_775;
            this.window.Read(param1, lookup);
            this.var_4005 = lookup.Lookup(param1) as class_954;
            this.var_4005.Read(param1, lookup);
            this.paymentLink = param1.ReadUTF();
            this.var_3289 = param1.ReadBoolean();
            this.visibility = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.var_521);
            param1.WriteShort(29672);
            param1.WriteShort(-6099);
            this.window.Write(param1);
            this.var_4005.Write(param1);
            param1.WriteUTF(this.paymentLink);
            param1.WriteBoolean(this.var_3289);
            param1.WriteShort(this.visibility);
        }
    }
}
