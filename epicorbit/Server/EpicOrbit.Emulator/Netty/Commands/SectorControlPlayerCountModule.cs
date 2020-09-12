using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SectorControlPlayerCountModule : ICommand {

        public short ID { get; set; } = 1545;
        public int playerCount = 0;
        public int maxPlayers = 0;
        public FactionModule faction;

        public SectorControlPlayerCountModule(FactionModule param1 = null, int param2 = 0, int param3 = 0) {
            if (param1 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param1;
            }
            this.playerCount = param2;
            this.maxPlayers = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.playerCount = param1.ReadInt();
            this.playerCount = param1.Shift(this.playerCount, 6);
            param1.ReadShort();
            this.maxPlayers = param1.ReadInt();
            this.maxPlayers = param1.Shift(this.maxPlayers, 12);
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.playerCount, 26));
            param1.WriteShort(1209);
            param1.WriteInt(param1.Shift(this.maxPlayers, 20));
            this.faction.Write(param1);
        }
    }
}
