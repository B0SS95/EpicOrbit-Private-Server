using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class CompanyHierarchyRankingModule : ICommand {

        public short ID { get; set; } = 14366;
        public string cbsNamesAndLocations = "";
        public int rank = 0;
        public int clanId = 0;
        public int rankingPoints = 0;
        public string leaderName = "";
        public string clanName = "";

        public CompanyHierarchyRankingModule(int param1 = 0, int param2 = 0, string param3 = "", string param4 = "", string param5 = "", int param6 = 0) {
            this.clanId = param1;
            this.rank = param2;
            this.clanName = param3;
            this.leaderName = param4;
            this.cbsNamesAndLocations = param5;
            this.rankingPoints = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.cbsNamesAndLocations = param1.ReadUTF();
            this.rank = param1.ReadInt();
            this.rank = param1.Shift(this.rank, 7);
            this.clanId = param1.ReadInt();
            this.clanId = param1.Shift(this.clanId, 13);
            param1.ReadShort();
            this.rankingPoints = param1.ReadInt();
            this.rankingPoints = param1.Shift(this.rankingPoints, 20);
            this.leaderName = param1.ReadUTF();
            this.clanName = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteUTF(this.cbsNamesAndLocations);
            param1.WriteInt(param1.Shift(this.rank, 25));
            param1.WriteInt(param1.Shift(this.clanId, 19));
            param1.WriteShort(31271);
            param1.WriteInt(param1.Shift(this.rankingPoints, 12));
            param1.WriteUTF(this.leaderName);
            param1.WriteUTF(this.clanName);
        }
    }
}
