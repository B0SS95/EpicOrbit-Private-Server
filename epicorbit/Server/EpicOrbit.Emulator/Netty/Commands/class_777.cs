using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_777 : class_505, ICommand {

        public override short ID { get; set; } = 27878;
        public string name_176 = "";
        public class_653 var_529;
        public class_1085 var_2394;
        public string name_165 = "";
        public int var_977 = 0;
        public int name_159 = 0;

        public class_777(int param1 = 0, string param2 = "", class_653 param3 = null, string param4 = "", class_1085 param5 = null, int param6 = 0) {
            this.name_176 = param2;
            this.name_165 = param4;
            this.name_159 = param1;
            this.var_977 = param6;
            if (param3 == null) {
                this.var_529 = new class_653();
            } else {
                this.var_529 = param3;
            }
            if (param5 == null) {
                this.var_2394 = new class_1085();
            } else {
                this.var_2394 = param5;
            }
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.name_176 = param1.ReadUTF();
            this.var_529 = lookup.Lookup(param1) as class_653;
            this.var_529.Read(param1, lookup);
            param1.ReadShort();
            this.var_2394 = lookup.Lookup(param1) as class_1085;
            this.var_2394.Read(param1, lookup);
            this.name_165 = param1.ReadUTF();
            this.var_977 = param1.ReadInt();
            this.var_977 = param1.Shift(this.var_977, 11);
            this.name_159 = param1.ReadInt();
            this.name_159 = param1.Shift(this.name_159, 23);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteUTF(this.name_176);
            this.var_529.Write(param1);
            param1.WriteShort(-2229);
            this.var_2394.Write(param1);
            param1.WriteUTF(this.name_165);
            param1.WriteInt(param1.Shift(this.var_977, 21));
            param1.WriteInt(param1.Shift(this.name_159, 9));
        }
    }
}
