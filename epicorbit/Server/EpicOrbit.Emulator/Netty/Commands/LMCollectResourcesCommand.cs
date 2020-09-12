using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LMCollectResourcesCommand : ICommand {

        public short ID { get; set; } = 32294;
        public List<OreCountModule> contentList;
        public LogMessengerPriorityModule priorityMode;

        public LMCollectResourcesCommand(LogMessengerPriorityModule param1 = null, List<OreCountModule> param2 = null) {
            if (param1 == null) {
                this.priorityMode = new LogMessengerPriorityModule();
            } else {
                this.priorityMode = param1;
            }
            if (param2 == null) {
                this.contentList = new List<OreCountModule>();
            } else {
                this.contentList = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.contentList.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as OreCountModule;
                tmp_0.Read(param1, lookup);
                this.contentList.Add(tmp_0);
            }
            param1.ReadShort();
            this.priorityMode = lookup.Lookup(param1) as LogMessengerPriorityModule;
            this.priorityMode.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.contentList.Count);
            foreach (var tmp_0 in this.contentList) {
                tmp_0.Write(param1);
            }
            param1.WriteShort(-18347);
            this.priorityMode.Write(param1);
            param1.WriteShort(18436);
        }
    }
}
