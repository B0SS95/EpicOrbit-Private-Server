using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_767 : class_752, ICommand {

        public override short ID { get; set; } = 32089;
        public string var_4513 = "";

        public class_767(string param1 = "", string param2 = "", int param3 = 0, int param4 = 0)
         : base(param2, param4, param3) {
            this.var_4513 = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.var_4513 = param1.ReadUTF();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteUTF(this.var_4513);
            param1.WriteShort(-8434);
        }
    }
}
