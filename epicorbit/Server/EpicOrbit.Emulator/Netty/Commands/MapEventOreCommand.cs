using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class MapEventOreCommand : ICommand {

        public const short COLLECTION_IN_PROGRESS = 0;
        public const short COLLECTION_FAILED_ALREADY_COLLECTED = 3;
        public const short COLLECTION_FAILED_CARGO_FULL = 2;
        public const short COLLECTION_FINISHED = 1;
        public short ID { get; set; } = 29752;
        public short eventType = 0;
        public OreTypeModule oreType;
        public string hash = "";

        public MapEventOreCommand(short param1 = 0, OreTypeModule param2 = null, string param3 = "") {
            this.eventType = param1;
            if (param2 == null) {
                this.oreType = new OreTypeModule();
            } else {
                this.oreType = param2;
            }
            this.hash = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.eventType = param1.ReadShort();
            this.oreType = lookup.Lookup(param1) as OreTypeModule;
            this.oreType.Read(param1, lookup);
            this.hash = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(13131);
            param1.WriteShort(this.eventType);
            this.oreType.Write(param1);
            param1.WriteUTF(this.hash);
        }
    }
}
