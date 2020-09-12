using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UILayerAdvertisementCommand : ICommand {

        public short ID { get; set; } = 581;
        public bool moveable = false;
        public string advertisementKey = "";
        public AlignmentModule alignment;
        public bool closeable = false;

        public UILayerAdvertisementCommand(AlignmentModule param1 = null, bool param2 = false, bool param3 = false, string param4 = "") {
            if (param1 == null) {
                this.alignment = new AlignmentModule();
            } else {
                this.alignment = param1;
            }
            this.closeable = param2;
            this.moveable = param3;
            this.advertisementKey = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.moveable = param1.ReadBoolean();
            this.advertisementKey = param1.ReadUTF();
            this.alignment = lookup.Lookup(param1) as AlignmentModule;
            this.alignment.Read(param1, lookup);
            param1.ReadShort();
            this.closeable = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.moveable);
            param1.WriteUTF(this.advertisementKey);
            this.alignment.Write(param1);
            param1.WriteShort(30347);
            param1.WriteBoolean(this.closeable);
        }
    }
}
