using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class WordPuzzleLetterAchievedCommand : ICommand {

        public short ID { get; set; } = 22366;
        public bool complete = false;
        public List<WordPuzzleLetterModule> letterValues;

        public WordPuzzleLetterAchievedCommand(List<WordPuzzleLetterModule> param1 = null, bool param2 = false) {
            if (param1 == null) {
                this.letterValues = new List<WordPuzzleLetterModule>();
            } else {
                this.letterValues = param1;
            }
            this.complete = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.complete = param1.ReadBoolean();
            this.letterValues.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as WordPuzzleLetterModule;
                tmp_0.Read(param1, lookup);
                this.letterValues.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.complete);
            param1.WriteInt(this.letterValues.Count);
            foreach (var tmp_0 in this.letterValues) {
                tmp_0.Write(param1);
            }
        }
    }
}
