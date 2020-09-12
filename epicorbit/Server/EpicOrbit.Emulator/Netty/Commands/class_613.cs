using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_613 : ICommand {

        public short ID { get; set; } = 19740;
        public int score = 0;
        public FactionModule faction;

        public class_613(FactionModule param1 = null, int param2 = 0) {
            if (param1 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param1;
            }
            this.score = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.score = param1.ReadInt();
            this.score = param1.Shift(this.score, 22);
            param1.ReadShort();
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.score, 10));
            param1.WriteShort(-11405);
            this.faction.Write(param1);
        }
    }
}
