using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UpdateItemModule : ICommand {

        public short ID { get; set; } = 23576;
        public LabItemModule itemToUpdate;
        public OreCountModule oreCountToUpdateWith;

        public UpdateItemModule(LabItemModule param1 = null, OreCountModule param2 = null) {
            if (param1 == null) {
                this.itemToUpdate = new LabItemModule();
            } else {
                this.itemToUpdate = param1;
            }
            if (param2 == null) {
                this.oreCountToUpdateWith = new OreCountModule();
            } else {
                this.oreCountToUpdateWith = param2;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.itemToUpdate = lookup.Lookup(param1) as LabItemModule;
            this.itemToUpdate.Read(param1, lookup);
            param1.ReadShort();
            this.oreCountToUpdateWith = lookup.Lookup(param1) as OreCountModule;
            this.oreCountToUpdateWith.Read(param1, lookup);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            this.itemToUpdate.Write(param1);
            param1.WriteShort(30411);
            this.oreCountToUpdateWith.Write(param1);
        }
    }
}
