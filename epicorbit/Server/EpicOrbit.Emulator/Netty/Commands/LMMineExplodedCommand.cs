using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMMineExplodedCommand : ICommand {

        public short ID { get; set; } = 24737;
        public string hash = "";
        public LogMessengerPriorityModule priorityMode;

        public LMMineExplodedCommand(LogMessengerPriorityModule param1 = null, string param2 = "") {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            this.hash = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.hash = param1.ReadUTF();
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(18189);
            param1.WriteUTF(this.hash);
            this.priorityMode.Write(param1);
            param1.WriteShort(24144);
        }
    }
}
