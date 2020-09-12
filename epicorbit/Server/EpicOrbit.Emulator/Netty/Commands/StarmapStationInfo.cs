using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class StarmapStationInfo : ICommand {

        public const short ASTEROID = 0;
        public const short HOSTILE_STATION = 3;
        public const short OWN_STATION = 1;
        public const short NEUTRAL_STATION = 2;
        public short ID { get; set; } = 19869;
        public FactionModule owningFaction;
        public short status = 0;
        public double lastChangedTimestamp = 0;
        public int xPositionPercentage = 0;
        public string asteroidName = "";
        public int yPositionPercentage = 0;
        public int mapId = 0;
        public string clanName = "";

        public StarmapStationInfo(int param1 = 0, int param2 = 0, int param3 = 0, short param4 = 0, double param5 = 0, string param6 = "", string param7 = "", FactionModule param8 = null) {
            this.mapId = param1;
            this.xPositionPercentage = param2;
            this.yPositionPercentage = param3;
            this.status = param4;
            this.lastChangedTimestamp = param5;
            this.clanName = param6;
            this.asteroidName = param7;
            if (param8 == null) {
                this.owningFaction = new FactionModule();
            } else {
                this.owningFaction = param8;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.owningFaction = lookup.Lookup(param1) as FactionModule;
            this.owningFaction.Read(param1, lookup);
            this.status = param1.ReadShort();
            this.lastChangedTimestamp = param1.ReadDouble();
            this.xPositionPercentage = param1.ReadInt();
            this.xPositionPercentage = param1.Shift(this.xPositionPercentage, 2);
            this.asteroidName = param1.ReadUTF();
            this.yPositionPercentage = param1.ReadInt();
            this.yPositionPercentage = param1.Shift(this.yPositionPercentage, 19);
            this.mapId = param1.ReadInt();
            this.mapId = param1.Shift(this.mapId, 5);
            this.clanName = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.owningFaction.Write(param1);
            param1.WriteShort(this.status);
            param1.WriteDouble(this.lastChangedTimestamp);
            param1.WriteInt(param1.Shift(this.xPositionPercentage, 30));
            param1.WriteUTF(this.asteroidName);
            param1.WriteInt(param1.Shift(this.yPositionPercentage, 13));
            param1.WriteInt(param1.Shift(this.mapId, 27));
            param1.WriteUTF(this.clanName);
        }
    }
}
