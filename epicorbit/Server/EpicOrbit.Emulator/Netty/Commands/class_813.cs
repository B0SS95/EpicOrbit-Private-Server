using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_813 : ICommand {

        public short ID { get; set; } = 2698;
        public double amount = 0;
        public string var_909 = "";

        public class_813(string param1 = "", double param2 = 0) {
            this.var_909 = param1;
            this.amount = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.amount = param1.ReadDouble();
            param1.ReadShort();
            this.var_909 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-20891);
            param1.WriteDouble(this.amount);
            param1.WriteShort(-10614);
            param1.WriteUTF(this.var_909);
        }
    }
}
