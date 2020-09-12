using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestChallengeRatingModule : ICommand {

        public short ID { get; set; } = 29961;
        public int rating = 0;
        public string name = "";
        public int diffToFirst = 0;
        public int rank = 0;
        public string epppLink = "";

        public QuestChallengeRatingModule(string param1 = "", string param2 = "", int param3 = 0, int param4 = 0, int param5 = 0) {
            this.name = param1;
            this.epppLink = param2;
            this.rank = param3;
            this.rating = param4;
            this.diffToFirst = param5;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.rating = param1.ReadInt();
            this.rating = param1.Shift(this.rating, 20);
            this.name = param1.ReadUTF();
            param1.ReadShort();
            this.diffToFirst = param1.ReadInt();
            this.diffToFirst = param1.Shift(this.diffToFirst, 3);
            this.rank = param1.ReadInt();
            this.rank = param1.Shift(this.rank, 5);
            this.epppLink = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(1525);
            param1.WriteInt(param1.Shift(this.rating, 12));
            param1.WriteUTF(this.name);
            param1.WriteShort(-15358);
            param1.WriteInt(param1.Shift(this.diffToFirst, 29));
            param1.WriteInt(param1.Shift(this.rank, 27));
            param1.WriteUTF(this.epppLink);
        }
    }
}
