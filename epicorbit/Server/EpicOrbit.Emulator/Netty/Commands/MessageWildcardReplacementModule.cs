using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MessageWildcardReplacementModule : ICommand {

        public short ID { get; set; } = 21860;
        public string wildcard = "";
        public ClientUITooltipTextFormatModule var_2363;
        public string replacement = "";

        public MessageWildcardReplacementModule(string param1 = "", string param2 = "", ClientUITooltipTextFormatModule param3 = null) {
            this.wildcard = param1;
            this.replacement = param2;
            if (param3 == null) {
                this.var_2363 = new ClientUITooltipTextFormatModule();
            } else {
                this.var_2363 = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.wildcard = param1.ReadUTF();
            this.var_2363 = lookup.Lookup(param1) as ClientUITooltipTextFormatModule;
            this.var_2363.Read(param1, lookup);
            this.replacement = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.wildcard);
            this.var_2363.Write(param1);
            param1.WriteUTF(this.replacement);
        }
    }
}
