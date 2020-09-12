using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUIMenuBarItemModule : ICommand {

        public virtual short ID { get; set; } = 30949;
        public bool visible = false;
        public string itemId = "";
        public ClientUITooltipsCommand toolTip;

        public ClientUIMenuBarItemModule(string param1 = "", bool param2 = false, ClientUITooltipsCommand param3 = null) {
            this.itemId = param1;
            this.visible = param2;
            if (param3 == null) {
                this.toolTip = new ClientUITooltipsCommand();
            } else {
                this.toolTip = param3;
            }
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            this.visible = param1.ReadBoolean();
            this.itemId = param1.ReadUTF();
            this.toolTip = lookup.Lookup(param1) as ClientUITooltipsCommand;
            this.toolTip.Read(param1, lookup);
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.visible);
            param1.WriteUTF(this.itemId);
            this.toolTip.Write(param1);
        }
    }
}
