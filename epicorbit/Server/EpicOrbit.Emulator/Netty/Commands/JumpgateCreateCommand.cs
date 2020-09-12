using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class JumpgateCreateCommand : ICommand {

        public short ID { get; set; } = 7257;
        public List<int> modificator;
        public bool isVisible = false;
        public int designId = 0;
        public int factionId = 0;
        public bool assetString = false;
        public int gateId = 0;
        public int y = 0;
        public int x = 0;

        public JumpgateCreateCommand(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, bool param6 = false, bool param7 = false, List<int> param8 = null) {
            this.gateId = param1;
            this.factionId = param2;
            this.designId = param3;
            this.x = param4;
            this.y = param5;
            this.isVisible = param6;
            this.assetString = param7;
            if (param8 == null) {
                this.modificator = new List<int>();
            } else {
                this.modificator = param8;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.modificator.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 16);
                this.modificator.Add(tmp_0);
            }
            this.isVisible = param1.ReadBoolean();
            this.designId = param1.ReadInt();
            this.designId = param1.Shift(this.designId, 4);
            this.factionId = param1.ReadInt();
            this.factionId = param1.Shift(this.factionId, 31);
            this.assetString = param1.ReadBoolean();
            this.gateId = param1.ReadInt();
            this.gateId = param1.Shift(this.gateId, 21);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 22);
            param1.ReadShort();
            param1.ReadShort();
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.modificator.Count);
            foreach (var tmp_0 in this.modificator) {
                param1.WriteInt(param1.Shift(tmp_0, 16));
            }
            param1.WriteBoolean(this.isVisible);
            param1.WriteInt(param1.Shift(this.designId, 28));
            param1.WriteInt(param1.Shift(this.factionId, 1));
            param1.WriteBoolean(this.assetString);
            param1.WriteInt(param1.Shift(this.gateId, 11));
            param1.WriteInt(param1.Shift(this.y, 10));
            param1.WriteShort(8737);
            param1.WriteShort(19248);
            param1.WriteInt(param1.Shift(this.x, 24));
        }
    }
}
