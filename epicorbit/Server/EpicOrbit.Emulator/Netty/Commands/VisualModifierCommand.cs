using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class VisualModifierCommand : ICommand {

        public const short const_975 = 39;
        public const short const_3403 = 6;
        public const short const_3367 = 2;
        public const short const_2253 = 15;
        public const short const_892 = 4;
        public const short const_1895 = 66;
        public const short const_1491 = 47;
        public const short const_350 = 43;
        public const short const_2087 = 35;
        public const short const_222 = 20;
        public const short const_1013 = 30;
        public const short const_2337 = 26;
        public const short const_278 = 44;
        public const short const_680 = 36;
        public const short const_3301 = 37;
        public const short const_721 = 0;
        public const short const_3208 = 38;
        public const short const_2646 = 53;
        public const short const_3146 = 16;
        public const short const_3095 = 60;
        public const short const_2673 = 51;
        public const short const_1783 = 8;
        public const short const_3410 = 34;
        public const short const_413 = 3;
        public const short INVINCIBILITY = 32;
        public const short const_881 = 61;
        public const short const_2163 = 7;
        public const short const_1523 = 17;
        public const short const_662 = 11;
        public const short const_807 = 56;
        public const short const_1305 = 45;
        public const short const_665 = 28;
        public const short const_480 = 21;
        public const short const_1383 = 13;
        public const short const_2362 = 62;
        public const short const_1001 = 24;
        public const short const_98 = 33;
        public const short const_545 = 22;
        public const short const_2342 = 12;
        public const short const_64 = 41;
        public const short const_1770 = 58;
        public const short const_223 = 23;
        public const short const_1166 = 18;
        public const short const_2383 = 5;
        public const short const_1273 = 40;
        public const short const_2169 = 57;
        public const short const_556 = 59;
        public const short const_1269 = 64;
        public const short const_2291 = 10;
        public const short INACTIVE = 14;
        public const short const_2026 = 27;
        public const short const_3012 = 31;
        public const short const_779 = 54;
        public const short const_3402 = 25;
        public const short const_2252 = 29;
        public const short SINGULARITY = 19;
        public const short const_961 = 63;
        public const short const_1813 = 52;
        public const short const_476 = 48;
        public const short const_1115 = 9;
        public const short const_470 = 65;
        public const short const_2329 = 42;
        public const short const_3478 = 55;
        public const short const_1478 = 50;
        public const short const_1333 = 46;
        public const short const_1221 = 1;
        public const short const_1411 = 49;
        public short ID { get; set; } = 27040;
        public short modifier = 0;
        public bool activated = false;
        public int attribute = 0;
        public int count = 0;
        public int userId = 0;
        public string var_2655 = "";

        public VisualModifierCommand(int param1 = 0, short param2 = 0, int param3 = 0, string param4 = "", int param5 = 0, bool param6 = false) {
            this.userId = param1;
            this.modifier = param2;
            this.attribute = param3;
            this.var_2655 = param4;
            this.count = param5;
            this.activated = param6;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.modifier = param1.ReadShort();
            this.activated = param1.ReadBoolean();
            this.attribute = param1.ReadInt();
            this.attribute = param1.Shift(this.attribute, 3);
            this.count = param1.ReadInt();
            this.count = param1.Shift(this.count, 3);
            this.userId = param1.ReadInt();
            this.userId = param1.Shift(this.userId, 1);
            this.var_2655 = param1.ReadUTF();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.modifier);
            param1.WriteBoolean(this.activated);
            param1.WriteInt(param1.Shift(this.attribute, 29));
            param1.WriteInt(param1.Shift(this.count, 29));
            param1.WriteInt(param1.Shift(this.userId, 31));
            param1.WriteUTF(this.var_2655);
        }
    }
}
