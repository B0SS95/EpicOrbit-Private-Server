using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class class_660 : ICommand {

        public short ID { get; set; } = 19153;
        public bool name_168 = false;
        public class_571 var_3927;
        public bool cloaked = false;
        public class_550 target;
        public int id = 0;
        public int level = 0;
        public class_657 location;
        public string name = "";
        public class_908 clan;
        public bool var_5315 = false;
        public class_504 var_3152;
        public class_588 var_2874;
        public bool active = false;
        public FactionModule faction;
        public bool var_2632 = false;

        public class_660(string param1 = "", int param2 = 0, class_588 param3 = null, class_657 param4 = null, int param5 = 0, bool param6 = false, bool param7 = false, bool param8 = false, bool param9 = false, bool param10 = false, class_908 param11 = null, FactionModule param12 = null, class_550 param13 = null, class_504 param14 = null, class_571 param15 = null) {
            this.name = param1;
            this.id = param2;
            if (param3 == null) {
                this.var_2874 = new class_588();
            } else {
                this.var_2874 = param3;
            }
            if (param4 == null) {
                this.location = new class_657();
            } else {
                this.location = param4;
            }
            this.level = param5;
            this.active = param6;
            this.cloaked = param7;
            this.name_168 = param8;
            this.var_5315 = param9;
            this.var_2632 = param10;
            if (param11 == null) {
                this.clan = new class_908();
            } else {
                this.clan = param11;
            }
            if (param12 == null) {
                this.faction = new FactionModule();
            } else {
                this.faction = param12;
            }
            if (param13 == null) {
                this.target = new class_550();
            } else {
                this.target = param13;
            }
            if (param14 == null) {
                this.var_3152 = new class_504();
            } else {
                this.var_3152 = param14;
            }
            if (param15 == null) {
                this.var_3927 = new class_571();
            } else {
                this.var_3927 = param15;
            }
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.name_168 = param1.ReadBoolean();
            this.var_3927 = lookup.Lookup(param1) as class_571;
            this.var_3927.Read(param1, lookup);
            this.cloaked = param1.ReadBoolean();
            this.target = lookup.Lookup(param1) as class_550;
            this.target.Read(param1, lookup);
            this.id = param1.ReadInt();
            this.id = param1.Shift(this.id, 17);
            this.level = param1.ReadInt();
            this.level = param1.Shift(this.level, 5);
            this.location = lookup.Lookup(param1) as class_657;
            this.location.Read(param1, lookup);
            this.name = param1.ReadUTF();
            this.clan = lookup.Lookup(param1) as class_908;
            this.clan.Read(param1, lookup);
            this.var_5315 = param1.ReadBoolean();
            this.var_3152 = lookup.Lookup(param1) as class_504;
            this.var_3152.Read(param1, lookup);
            this.var_2874 = lookup.Lookup(param1) as class_588;
            this.var_2874.Read(param1, lookup);
            this.active = param1.ReadBoolean();
            this.faction = lookup.Lookup(param1) as FactionModule;
            this.faction.Read(param1, lookup);
            this.var_2632 = param1.ReadBoolean();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteBoolean(this.name_168);
            this.var_3927.Write(param1);
            param1.WriteBoolean(this.cloaked);
            this.target.Write(param1);
            param1.WriteInt(param1.Shift(this.id, 15));
            param1.WriteInt(param1.Shift(this.level, 27));
            this.location.Write(param1);
            param1.WriteUTF(this.name);
            this.clan.Write(param1);
            param1.WriteBoolean(this.var_5315);
            this.var_3152.Write(param1);
            this.var_2874.Write(param1);
            param1.WriteBoolean(this.active);
            this.faction.Write(param1);
            param1.WriteBoolean(this.var_2632);
        }
    }
}
