using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_921 : class_540, ICommand {

        public override short ID { get; set; } = 5404;
        public string lootId = "";

        public class_921(string param1 = "", string param2 = "")
         : base(param2) {
            this.lootId = param1;
        }

        public override void Read(IDataInput param1, ICommandLookup lookup) {
            base.Read(param1, lookup);
            this.lootId = param1.ReadUTF();
            param1.ReadShort();
        }

        public override void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected override void method_9(IDataOutput param1) {
            base.method_9(param1);
            param1.WriteUTF(this.lootId);
            param1.WriteShort(-20989);
        }
    }
}
