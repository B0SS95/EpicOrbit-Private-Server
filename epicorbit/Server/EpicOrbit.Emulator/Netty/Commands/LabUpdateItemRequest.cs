using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class LabUpdateItemRequest : ICommand {

        public short ID { get; set; } = 29252;
        public LabItemModule itemToUpdate;
        public OreCountModule updateWith;

        public LabUpdateItemRequest(LabItemModule param1 = null, OreCountModule param2 = null) {
            if (param1 == null) {
                this.itemToUpdate = new LabItemModule();
            } else {
                this.itemToUpdate = param1;
            }
            if (param2 == null) {
                this.updateWith = new OreCountModule();
            } else {
                this.updateWith = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemToUpdate = lookup.Lookup(param1) as LabItemModule;
            this.itemToUpdate.Read(param1, lookup);
            this.updateWith = lookup.Lookup(param1) as OreCountModule;
            this.updateWith.Read(param1, lookup);
            param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.itemToUpdate.Write(param1);
            this.updateWith.Write(param1);
            param1.WriteShort(25453);
        }
    }
}
