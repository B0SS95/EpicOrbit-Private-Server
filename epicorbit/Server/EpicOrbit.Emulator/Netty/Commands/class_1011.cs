using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1011 : ICommand {

        public short ID { get; set; } = 16627;
        public class_954 var_4005;
        public int var_3378 = 0;

        public class_1011(int param1 = 0, class_954 param2 = null) {
            this.var_3378 = param1;
            if (param2 == null) {
                this.var_4005 = new class_954();
            } else {
                this.var_4005 = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_4005 = lookup.Lookup(param1) as class_954;
            this.var_4005.Read(param1, lookup);
            param1.ReadShort();
            this.var_3378 = param1.ReadInt();
            this.var_3378 = param1.Shift(this.var_3378, 21);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_4005.Write(param1);
            param1.WriteShort(-15106);
            param1.WriteInt(param1.Shift(this.var_3378, 11));
        }
    }
}
