using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUITooltipModule : ICommand {

        public const short RED = 1;
        public const short STANDARD = 0;
        public short ID { get; set; } = 11959;
        public ClientUITooltipTextFormatModule var_5158;
        public List<MessageWildcardReplacementModule> var_4500;
        public string baseKey = "";
        public short var_2555 = 0;

        public ClientUITooltipModule(short param1 = 0, string param2 = "", ClientUITooltipTextFormatModule param3 = null, List<MessageWildcardReplacementModule> param4 = null) {
            this.var_2555 = param1;
            this.baseKey = param2;
            if (param3 == null) {
                this.var_5158 = new ClientUITooltipTextFormatModule();
            } else {
                this.var_5158 = param3;
            }
            if (param4 == null) {
                this.var_4500 = new List<MessageWildcardReplacementModule>();
            } else {
                this.var_4500 = param4;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_5158 = lookup.Lookup(param1) as ClientUITooltipTextFormatModule;
            this.var_5158.Read(param1, lookup);
            param1.ReadShort();
            this.var_4500.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as MessageWildcardReplacementModule;
                tmp_0.Read(param1, lookup);
                this.var_4500.Add(tmp_0);
            }
            this.baseKey = param1.ReadUTF();
            this.var_2555 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.var_5158.Write(param1);
            param1.WriteShort(3559);
            param1.WriteInt(this.var_4500.Count);
            foreach (var tmp_0 in this.var_4500) {
                tmp_0.Write(param1);
            }
            param1.WriteUTF(this.baseKey);
            param1.WriteShort(this.var_2555);
        }
    }
}
