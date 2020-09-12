using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class SelectMenuBarItem : ICommand {

        public const short const_2764 = 1;
        public const short const_2981 = 0;
        public const short ACTIVATE = 1;
        public const short SELECT = 0;
        public short ID { get; set; } = 14787;
        public short var_2253 = 0;
        public short doubleClick = 0;
        public string ItemId = "";

        public SelectMenuBarItem(string param1 = "", short param2 = 0, short param3 = 0) {
            this.ItemId = param1;
            this.doubleClick = param2;
            this.var_2253 = param3;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.var_2253 = param1.ReadShort();
            this.doubleClick = param1.ReadShort();
            this.ItemId = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.var_2253);
            param1.WriteShort(this.doubleClick);
            param1.WriteUTF(this.ItemId);
        }
    }
}
