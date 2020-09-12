using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_1004 : ICommand {

        public short ID { get; set; } = 12887;
        public int upgradeLevel = 0;
        public double amount = 0;
        public List<class_586> var_1549;
        public double itemId = 0;
        public string lootId = "";
        public bool isNew = false;

        public class_1004(double param1 = 0, string param2 = "", double param3 = 0, int param4 = 0, bool param5 = false, List<class_586> param6 = null) {
            this.itemId = param1;
            this.lootId = param2;
            this.amount = param3;
            this.upgradeLevel = param4;
            this.isNew = param5;
            if (param6 == null) {
                this.var_1549 = new List<class_586>();
            } else {
                this.var_1549 = param6;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.upgradeLevel = param1.ReadInt();
            this.upgradeLevel = param1.Shift(this.upgradeLevel, 11);
            this.amount = param1.ReadDouble();
            this.var_1549.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_586;
                tmp_0.Read(param1, lookup);
                this.var_1549.Add(tmp_0);
            }
            this.itemId = param1.ReadDouble();
            this.lootId = param1.ReadUTF();
            this.isNew = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(8616);
            param1.WriteInt(param1.Shift(this.upgradeLevel, 21));
            param1.WriteDouble(this.amount);
            param1.WriteInt(this.var_1549.Count);
            foreach (var tmp_0 in this.var_1549) {
                tmp_0.Write(param1);
            }
            param1.WriteDouble(this.itemId);
            param1.WriteUTF(this.lootId);
            param1.WriteBoolean(this.isNew);
        }
    }
}
