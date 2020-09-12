using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class AbilityStatusFullCommand : ICommand {

        public short ID { get; set; } = 17190;
        public List<AbilityStatusSingleCommand> abilities;

        public AbilityStatusFullCommand(List<AbilityStatusSingleCommand> param1 = null) {
            if (param1 == null) {
                this.abilities = new List<AbilityStatusSingleCommand>();
            } else {
                this.abilities = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.abilities.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as AbilityStatusSingleCommand;
                tmp_0.Read(param1, lookup);
                this.abilities.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(-18504);
            param1.WriteInt(this.abilities.Count);
            foreach (var tmp_0 in this.abilities) {
                tmp_0.Write(param1);
            }
        }
    }
}
