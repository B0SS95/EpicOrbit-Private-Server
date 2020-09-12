using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_652 : ICommand {

        public short ID { get; set; } = 20996;
        public class_547 status;
        public string lootId = "";

        public class_652(string param1 = "", class_547 param2 = null) {
            this.lootId = param1;
            if (param2 == null) {
                this.status = new class_547();
            } else {
                this.status = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.status = lookup.Lookup(param1) as class_547;
            this.status.Read(param1, lookup);
            this.lootId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.status.Write(param1);
            param1.WriteUTF(this.lootId);
        }
    }
}
