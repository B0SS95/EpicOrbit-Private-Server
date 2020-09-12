using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class KillScreenRepairRequest : ICommand {

        public short ID { get; set; } = 11432;
        public KillScreenOptionTypeModule selection;
        public LoginRequest requestModule;

        public KillScreenRepairRequest(KillScreenOptionTypeModule param1 = null, LoginRequest param2 = null) {
            if (param1 == null) {
                this.selection = new KillScreenOptionTypeModule();
            } else {
                this.selection = param1;
            }
            if (param2 == null) {
                this.requestModule = new LoginRequest();
            } else {
                this.requestModule = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            param1.ReadShort();
            this.selection = lookup.Lookup(param1) as KillScreenOptionTypeModule;
            this.selection.Read(param1, lookup);
            this.requestModule = lookup.Lookup(param1) as LoginRequest;
            this.requestModule.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(27825);
            param1.WriteShort(14212);
            this.selection.Write(param1);
            this.requestModule.Write(param1);
        }
    }
}
