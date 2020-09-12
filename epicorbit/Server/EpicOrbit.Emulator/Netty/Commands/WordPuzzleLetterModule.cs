using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class WordPuzzleLetterModule : ICommand {

        public short ID { get; set; } = 7128;
        public int letterIndex = 0;
        public string letterValue = "";

        public WordPuzzleLetterModule(string param1 = "", int param2 = 0) {
            this.letterValue = param1;
            this.letterIndex = param2;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.letterIndex = param1.ReadInt();
            this.letterIndex = param1.Shift(this.letterIndex, 1);
            this.letterValue = param1.ReadUTF();
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.letterIndex, 31));
            param1.WriteUTF(this.letterValue);
            param1.WriteShort(20734);
        }
    }
}
