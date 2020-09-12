using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class QuestListUpdateCommand : ICommand {

        public short ID { get; set; } = 12799;
        public List<QuestSlimInfoModule> list;

        public QuestListUpdateCommand(List<QuestSlimInfoModule> param1 = null) {
            if (param1 == null) {
                this.list = new List<QuestSlimInfoModule>();
            } else {
                this.list = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.list.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as QuestSlimInfoModule;
                tmp_0.Read(param1, lookup);
                this.list.Add(tmp_0);
            }
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.list.Count);
            foreach (var tmp_0 in this.list) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-17360);
        }
    }
}
