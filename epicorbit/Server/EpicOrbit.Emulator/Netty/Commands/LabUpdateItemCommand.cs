using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LabUpdateItemCommand : ICommand {

        public short ID { get; set; } = 4719;
        public List<UpdateItemModule> itemsUpdatedInfo;

        public LabUpdateItemCommand(List<UpdateItemModule> param1 = null) {
            if (param1 == null) {
                this.itemsUpdatedInfo = new List<UpdateItemModule>();
            } else {
                this.itemsUpdatedInfo = param1;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemsUpdatedInfo.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as UpdateItemModule;
                tmp_0.Read(param1, lookup);
                this.itemsUpdatedInfo.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.itemsUpdatedInfo.Count);
            foreach (var tmp_0 in this.itemsUpdatedInfo) {
                tmp_0.Write(param1);
            }
        }
    }
}
