using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class VideoWindowCreateCommand : ICommand {

        public const short HELPMOVIE = 0;
        public const short COMMANDER = 1;

        public short ID { get; set; } = 10195;
        public short videoType = 0;
        public List<string> languageKeys;
        public int videoID = 0;
        public bool showButtons = false;
        public int windowID = 0;
        public string windowAlign = "";

        public VideoWindowCreateCommand(int param1 = 0, string param2 = "", bool param3 = false, List<string> param4 = null, int param5 = 0, short param6 = 0) {
            this.windowID = param1;
            this.windowAlign = param2;
            this.showButtons = param3;
            if (param4 == null) {
                this.languageKeys = new List<String>();
            } else {
                this.languageKeys = param4;
            }
            this.videoID = param5;
            this.videoType = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.videoType = param1.ReadShort();
            this.languageKeys.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.ReadUTF();
                this.languageKeys.Add(tmp_0);
            }
            this.videoID = param1.ReadInt();
            this.videoID = param1.Shift(this.videoID, 25);
            this.showButtons = param1.ReadBoolean();
            this.windowID = param1.ReadInt();
            this.windowID = param1.Shift(this.windowID, 14);
            this.windowAlign = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.videoType);
            param1.WriteInt(this.languageKeys.Count);
            foreach (var tmp_0 in this.languageKeys) {
                param1.WriteUTF(tmp_0);
            }
            param1.WriteInt(param1.Shift(this.videoID, 7));
            param1.WriteBoolean(this.showButtons);
            param1.WriteInt(param1.Shift(this.windowID, 18));
            param1.WriteUTF(this.windowAlign);
        }
    }
}
