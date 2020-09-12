using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UIWindowCreateCommand : ICommand {

        public short ID { get; set; } = 2659;
        public int videoId = 0;
        public List<string> textKeys;
        public int windowId = 0;
        public bool showButtons = false;
        public AlignmentModule alignement;

        public UIWindowCreateCommand(AlignmentModule param1 = null, int param2 = 0, int param3 = 0, bool param4 = false, List<string> param5 = null) {
            if (param1 == null) {
                this.alignement = new AlignmentModule();
            } else {
                this.alignement = param1;
            }
            this.windowId = param2;
            this.videoId = param3;
            this.showButtons = param4;
            if (param5 == null) {
                this.textKeys = new List<String>();
            } else {
                this.textKeys = param5;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.videoId = param1.ReadInt();
            this.videoId = param1.Shift(this.videoId, 21);
            this.textKeys.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.ReadUTF();
                this.textKeys.Add(tmp_0);
            }
            this.windowId = param1.ReadInt();
            this.windowId = param1.Shift(this.windowId, 26);
            this.showButtons = param1.ReadBoolean();
            this.alignement = lookup.Lookup(param1) as AlignmentModule;
            this.alignement.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.videoId, 11));
            param1.WriteInt(this.textKeys.Count);
            foreach (var tmp_0 in this.textKeys) {
                param1.WriteUTF(tmp_0);
            }
            param1.WriteInt(param1.Shift(this.windowId, 6));
            param1.WriteBoolean(this.showButtons);
            this.alignement.Write(param1);
        }
    }
}
