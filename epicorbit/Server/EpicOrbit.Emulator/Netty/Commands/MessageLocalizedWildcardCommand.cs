using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MessageLocalizedWildcardCommand : ICommand {

        public short ID { get; set; } = 1522;
        public List<MessageWildcardReplacementModule> wildCardReplacements;
        public string baseKey = "";
        public ClientUITooltipTextFormatModule var_5158;

        public MessageLocalizedWildcardCommand(string param1 = "", ClientUITooltipTextFormatModule param2 = null, List<MessageWildcardReplacementModule> param3 = null) {
            this.baseKey = param1;
            if (param2 == null) {
                this.var_5158 = new ClientUITooltipTextFormatModule();
            } else {
                this.var_5158 = param2;
            }
            if (param3 == null) {
                this.wildCardReplacements = new List<MessageWildcardReplacementModule>();
            } else {
                this.wildCardReplacements = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.wildCardReplacements.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as MessageWildcardReplacementModule;
                tmp_0.Read(param1, lookup);
                this.wildCardReplacements.Add(tmp_0);
            }
            param1.ReadShort();
            this.baseKey = param1.ReadUTF();
            param1.ReadShort();
            this.var_5158 = lookup.Lookup(param1) as ClientUITooltipTextFormatModule;
            this.var_5158.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.wildCardReplacements.Count);
            foreach (var tmp_0 in this.wildCardReplacements) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-4383);
            param1.WriteUTF(this.baseKey);
            param1.WriteShort(12659);
            this.var_5158.Write(param1);
        }
    }
}
