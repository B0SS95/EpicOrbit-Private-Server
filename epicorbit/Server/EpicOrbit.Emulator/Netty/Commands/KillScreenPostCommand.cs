using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class KillScreenPostCommand : ICommand {

        public short ID { get; set; } = 21407;
        public string killerEpppLink = "";
        public List<KillScreenOptionModule> options;
        public string killerName = "";
        public string playerShipAlias = "";
        public DestructionTypeModule cause;

        public KillScreenPostCommand(string param1 = "", string param2 = "", string param3 = "", DestructionTypeModule param4 = null, List<KillScreenOptionModule> param5 = null) {
            this.killerName = param1;
            this.killerEpppLink = param2;
            this.playerShipAlias = param3;
            if (param4 == null) {
                this.cause = new DestructionTypeModule();
            } else {
                this.cause = param4;
            }
            if (param5 == null) {
                this.options = new List<KillScreenOptionModule>();
            } else {
                this.options = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.killerEpppLink = param1.ReadUTF();
            this.options.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as KillScreenOptionModule;
                tmp_0.Read(param1, lookup);
                this.options.Add(tmp_0);
            }
            param1.ReadShort();
            this.killerName = param1.ReadUTF();
            this.playerShipAlias = param1.ReadUTF();
            this.cause = lookup.Lookup(param1) as DestructionTypeModule;
            this.cause.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.killerEpppLink);
            param1.WriteInt(this.options.Count);
            foreach (var tmp_0 in this.options) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(7696);
            param1.WriteUTF(this.killerName);
            param1.WriteUTF(this.playerShipAlias);
            this.cause.Write(param1);
        }
    }
}
