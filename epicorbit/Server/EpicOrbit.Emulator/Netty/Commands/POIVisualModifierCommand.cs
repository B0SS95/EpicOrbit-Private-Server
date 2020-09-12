using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class POIVisualModifierCommand : ICommand {

        public short ID { get; set; } = 2486;
        public string poiId = "";
        public VisualModifierCommand visualModifier;

        public POIVisualModifierCommand(string param1 = "", VisualModifierCommand param2 = null) {
            this.poiId = param1;
            if (param2 == null) {
                this.visualModifier = new VisualModifierCommand();
            } else {
                this.visualModifier = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.poiId = param1.ReadUTF();
            this.visualModifier = lookup.Lookup(param1) as VisualModifierCommand;
            this.visualModifier.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.poiId);
            this.visualModifier.Write(param1);
        }
    }
}
