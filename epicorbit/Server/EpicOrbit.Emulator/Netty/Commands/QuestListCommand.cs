using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestListCommand : ICommand {

        public short ID { get; set; } = 19641;
        public bool onlyStarter = false;
        public int maxEventQuests = 0;
        public int maxQuests = 0;
        public List<QuestSlimInfoModule> list;

        public QuestListCommand(List<QuestSlimInfoModule> param1 = null, bool param2 = false, int param3 = 0, int param4 = 0) {
            if (param1 == null) {
                this.list = new List<QuestSlimInfoModule>();
            } else {
                this.list = param1;
            }
            this.onlyStarter = param2;
            this.maxQuests = param3;
            this.maxEventQuests = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.onlyStarter = param1.ReadBoolean();
            this.maxEventQuests = param1.ReadInt();
            this.maxEventQuests = param1.Shift(this.maxEventQuests, 9);
            this.maxQuests = param1.ReadInt();
            this.maxQuests = param1.Shift(this.maxQuests, 1);
            this.list.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestSlimInfoModule;
                tmp_0.Read(param1, lookup);
                this.list.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(23180);
            param1.WriteBoolean(this.onlyStarter);
            param1.WriteInt(param1.Shift(this.maxEventQuests, 23));
            param1.WriteInt(param1.Shift(this.maxQuests, 31));
            param1.WriteInt(this.list.Count);
            foreach (var tmp_0 in this.list) {
                tmp_0.Write(param1);
            }
        }
    }
}
