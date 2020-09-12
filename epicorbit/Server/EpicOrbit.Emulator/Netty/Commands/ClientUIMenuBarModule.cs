using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class ClientUIMenuBarModule : ICommand {

        public const short GENERIC_FEATURE_BAR = 2;
        public const short GAME_FEATURE_BAR = 1;
        public const short NOT_ASSIGNED = 0;
        public short ID { get; set; } = 17866;
        public short menuId = 0;
        public string var_1193 = "";
        public string var_3108 = "";
        public List<ClientUIMenuBarItemModule> menuBarItems;

        public ClientUIMenuBarModule(short param1 = 0, List<ClientUIMenuBarItemModule> param2 = null, string param3 = "", string param4 = "") {
            this.menuId = param1;
            if (param2 == null) {
                this.menuBarItems = new List<ClientUIMenuBarItemModule>();
            } else {
                this.menuBarItems = param2;
            }
            this.var_3108 = param3;
            this.var_1193 = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.menuId = param1.ReadShort();
            this.var_1193 = param1.ReadUTF();
            param1.ReadShort();
            this.var_3108 = param1.ReadUTF();
            this.menuBarItems.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as ClientUIMenuBarItemModule;
                tmp_0.Read(param1, lookup);
                this.menuBarItems.Add(tmp_0);
            }
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.menuId);
            param1.WriteUTF(this.var_1193);
            param1.WriteShort(-10139);
            param1.WriteUTF(this.var_3108);
            param1.WriteInt(this.menuBarItems.Count);
            foreach (var tmp_0 in this.menuBarItems) {
                tmp_0.Write(param1);
            }
        }
    }
}
