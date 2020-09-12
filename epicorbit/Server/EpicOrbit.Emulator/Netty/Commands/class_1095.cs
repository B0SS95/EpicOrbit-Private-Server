using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1095 : ICommand {

        public short ID { get; set; } = 16052;
        public List<class_613> name_117;
        public int name_107 = 0;
        public int name_77 = 0;
        public List<class_613> name_158;
        public int name_96 = 0;
        public int name_55 = 0;

        public class_1095(List<class_613> param1 = null, int param2 = 0, int param3 = 0, int param4 = 0, int param5 = 0, List<class_613> param6 = null) {
            if (param1 == null) {
                this.name_158 = new List<class_613>();
            } else {
                this.name_158 = param1;
            }
            this.name_55 = param2;
            this.name_77 = param3;
            this.name_107 = param4;
            this.name_96 = param5;
            if (param6 == null) {
                this.name_117 = new List<class_613>();
            } else {
                this.name_117 = param6;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_117.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_613;
                tmp_0.Read(param1, lookup);
                this.name_117.Add(tmp_0);
            }
            this.name_107 = param1.ReadInt();
            this.name_107 = param1.Shift(this.name_107, 8);
            param1.ReadShort();
            this.name_77 = param1.ReadInt();
            this.name_77 = param1.Shift(this.name_77, 11);
            this.name_158.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_613;
                tmp_0.Read(param1, lookup);
                this.name_158.Add(tmp_0);
            }
            this.name_96 = param1.ReadInt();
            this.name_96 = param1.Shift(this.name_96, 29);
            this.name_55 = param1.ReadInt();
            this.name_55 = param1.Shift(this.name_55, 26);
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteInt(this.name_117.Count);
            foreach (var tmp_0 in this.name_117) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.name_107, 24));
            param1.WriteShort(14810);
            param1.WriteInt(param1.Shift(this.name_77, 21));
            param1.WriteInt(this.name_158.Count);
            foreach (var tmp_0 in this.name_158) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.name_96, 3));
            param1.WriteInt(param1.Shift(this.name_55, 6));
        }
    }
}
