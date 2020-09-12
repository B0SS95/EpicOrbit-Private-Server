using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1043 : class_752, ICommand {

        public override short ID { get; set; } = 17504;
        public int typeId = 0;
        public string var_4099 = "";
        public int var_3807 = 0;

        public class_1043(string param1 = "", string param2 = "", int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0)
         : base(param1, param6, param5) {
            this.typeId = param4;
            this.var_3807 = param3;
            this.var_4099 = param2;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.typeId = param1.ReadInt();
            this.typeId = param1.Shift(this.typeId, 12);
            this.var_4099 = param1.ReadUTF();
            this.var_3807 = param1.ReadInt();
            this.var_3807 = param1.Shift(this.var_3807, 21);
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteInt(param1.Shift(this.typeId, 20));
            param1.WriteUTF(this.var_4099);
            param1.WriteInt(param1.Shift(this.var_3807, 11));
        }
    }
}
