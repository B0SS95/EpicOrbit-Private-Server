using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestInfoCommand : ICommand {

        public short ID { get; set; } = 5599;
        public QuestDefinitionModule definition;
        public List<QuestChallengeRatingModule> ratings;
        public QuestChallengeRatingModule playersRating;

        public QuestInfoCommand(QuestDefinitionModule param1 = null, List<QuestChallengeRatingModule> param2 = null, QuestChallengeRatingModule param3 = null) {
            if (param1 == null) {
                this.definition = new QuestDefinitionModule();
            } else {
                this.definition = param1;
            }
            if (param2 == null) {
                this.ratings = new List<QuestChallengeRatingModule>();
            } else {
                this.ratings = param2;
            }
            if (param3 == null) {
                this.playersRating = new QuestChallengeRatingModule();
            } else {
                this.playersRating = param3;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.definition = lookup.Lookup(param1) as QuestDefinitionModule;
            this.definition.Read(param1, lookup);
            this.ratings.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestChallengeRatingModule;
                tmp_0.Read(param1, lookup);
                this.ratings.Add(tmp_0);
            }
            param1.ReadShort();
            this.playersRating = lookup.Lookup(param1) as QuestChallengeRatingModule;
            this.playersRating.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.definition.Write(param1);
            param1.WriteInt(this.ratings.Count);
            foreach (var tmp_0 in this.ratings) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-16401);
            this.playersRating.Write(param1);
            param1.WriteShort(-25406);
        }
    }
}
