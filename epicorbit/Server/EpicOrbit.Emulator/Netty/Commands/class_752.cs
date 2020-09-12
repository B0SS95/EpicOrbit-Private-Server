using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_752 : ICommand {

        public virtual short ID { get; set; } = 1665;
        public int x = 0;
        public int y = 0;
        public string hash = "";

        public class_752(string param1 = "", int param2 = 0, int param3 = 0) {
            this.hash = param1;
            this.x = param2;
            this.y = param3;
        }

        public virtual void Read(IDataInput param1, ICommandLookup lookup) {
            this.x = param1.ReadInt();
            this.x = param1.Shift(this.x, 6);
            this.y = param1.ReadInt();
            this.y = param1.Shift(this.y, 28);
            this.hash = param1.ReadUTF();
            param1.ReadShort();
        }

        public virtual void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected virtual void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.x, 26));
            param1.WriteInt(param1.Shift(this.y, 4));
            param1.WriteUTF(this.hash);
            param1.WriteShort(29244);
        }
    }
}
