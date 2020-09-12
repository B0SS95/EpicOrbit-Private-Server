using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_653 : ICommand {

        public short ID { get; set; } = 11442;
        public int name_73 = 0;
        public int name_133 = 0;
        public int name_172 = 0;
        public int name_75 = 0;
        public int name_61 = 0;
        public int name_144 = 0;
        public int name_50 = 0;
        public int name_85 = 0;
        public int name_5 = 0;
        public int name_92 = 0;

        public class_653(int param1 = 0, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, int param6 = 0, int param7 = 0, int param8 = 0, int param9 = 0, int param10 = 0) {
            this.name_5 = param1;
            this.name_50 = param2;
            this.name_85 = param3;
            this.name_73 = param4;
            this.name_172 = param5;
            this.name_75 = param6;
            this.name_61 = param7;
            this.name_133 = param8;
            this.name_144 = param9;
            this.name_92 = param10;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_73 = param1.ReadInt();
            this.name_73 = param1.Shift(this.name_73, 21);
            this.name_133 = param1.ReadInt();
            this.name_133 = param1.Shift(this.name_133, 17);
            this.name_172 = param1.ReadInt();
            this.name_172 = param1.Shift(this.name_172, 25);
            this.name_75 = param1.ReadInt();
            this.name_75 = param1.Shift(this.name_75, 2);
            this.name_61 = param1.ReadInt();
            this.name_61 = param1.Shift(this.name_61, 10);
            this.name_144 = param1.ReadInt();
            this.name_144 = param1.Shift(this.name_144, 27);
            this.name_50 = param1.ReadInt();
            this.name_50 = param1.Shift(this.name_50, 16);
            this.name_85 = param1.ReadInt();
            this.name_85 = param1.Shift(this.name_85, 13);
            this.name_5 = param1.ReadInt();
            this.name_5 = param1.Shift(this.name_5, 4);
            this.name_92 = param1.ReadInt();
            this.name_92 = param1.Shift(this.name_92, 8);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(param1.Shift(this.name_73, 11));
            param1.WriteInt(param1.Shift(this.name_133, 15));
            param1.WriteInt(param1.Shift(this.name_172, 7));
            param1.WriteInt(param1.Shift(this.name_75, 30));
            param1.WriteInt(param1.Shift(this.name_61, 22));
            param1.WriteInt(param1.Shift(this.name_144, 5));
            param1.WriteInt(param1.Shift(this.name_50, 16));
            param1.WriteInt(param1.Shift(this.name_85, 19));
            param1.WriteInt(param1.Shift(this.name_5, 28));
            param1.WriteInt(param1.Shift(this.name_92, 24));
        }
    }
}
