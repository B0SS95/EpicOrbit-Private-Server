using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_533 : ICommand {

        public const short const_515 = 3;
        public const short const_503 = 2;
        public const short WIN = 0;
        public const short const_2801 = 1;
        public short ID { get; set; } = 9000;
        public List<class_786> var_3653;
        public List<LootModule> rewards;
        public List<class_786> var_4269;
        public int var_573 = 0;
        public int var_1104 = 0;
        public FactionModule var_3191;
        public class_693 name_130;
        public FactionModule var_4496;
        public short var_2566 = 0;

        public class_533(short param1 = 0, int param2 = 0, int param3 = 0, FactionModule param4 = null, FactionModule param5 = null, List<class_786> param6 = null, List<class_786> param7 = null, List<LootModule> param8 = null, class_693 param9 = null) {
            this.var_2566 = param1;
            this.var_1104 = param2;
            this.var_573 = param3;
            if (param4 == null) {
                this.var_4496 = new FactionModule();
            } else {
                this.var_4496 = param4;
            }
            if (param5 == null) {
                this.var_3191 = new FactionModule();
            } else {
                this.var_3191 = param5;
            }
            if (param6 == null) {
                this.var_3653 = new List<class_786>();
            } else {
                this.var_3653 = param6;
            }
            if (param7 == null) {
                this.var_4269 = new List<class_786>();
            } else {
                this.var_4269 = param7;
            }
            if (param8 == null) {
                this.rewards = new List<LootModule>();
            } else {
                this.rewards = param8;
            }
            if (param9 == null) {
                this.name_130 = new class_693();
            } else {
                this.name_130 = param9;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            param1.ReadShort();
            this.var_3653.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_786;
                tmp_0.Read(param1, lookup);
                this.var_3653.Add(tmp_0);
            }
            this.rewards.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as LootModule;
                tmp_0.Read(param1, lookup);
                this.rewards.Add(tmp_0);
            }
            this.var_4269.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = lookup.Lookup(param1) as class_786;
                tmp_0.Read(param1, lookup);
                this.var_4269.Add(tmp_0);
            }
            this.var_573 = param1.ReadInt();
            this.var_573 = param1.Shift(this.var_573, 28);
            this.var_1104 = param1.ReadInt();
            this.var_1104 = param1.Shift(this.var_1104, 18);
            this.var_3191 = lookup.Lookup(param1) as FactionModule;
            this.var_3191.Read(param1, lookup);
            this.name_130 = lookup.Lookup(param1) as class_693;
            this.name_130.Read(param1, lookup);
            this.var_4496 = lookup.Lookup(param1) as FactionModule;
            this.var_4496.Read(param1, lookup);
            this.var_2566 = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(5155);
            param1.WriteInt(this.var_3653.Count);
            foreach (var tmp_0 in this.var_3653) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.rewards.Count);
            foreach (var tmp_0 in this.rewards) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(this.var_4269.Count);
            foreach (var tmp_0 in this.var_4269) {
                tmp_0.Write(param1);
            }
            param1.WriteInt(param1.Shift(this.var_573, 4));
            param1.WriteInt(param1.Shift(this.var_1104, 14));
            this.var_3191.Write(param1);
            this.name_130.Write(param1);
            this.var_4496.Write(param1);
            param1.WriteShort(this.var_2566);
        }
    }
}
