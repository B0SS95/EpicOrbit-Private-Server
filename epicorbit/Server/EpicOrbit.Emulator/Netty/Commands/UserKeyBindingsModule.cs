using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System.Collections.Generic;
namespace EpicOrbit.Emulator.Netty.Commands {

    [AutoDiscover("10.0.6435")]
    public class UserKeyBindingsModule : ICommand {

        public const short const_1091 = 14;
        public const short const_1475 = 16;
        public const short LOGOUT = 6;
        public const short PET_REPAIR_SHIP = 13;
        public const short PET_ACTIVATE = 4;
        public const short ZOOM_OUT = 12;
        public const short QUICKSLOT_PREMIUM = 8;
        public const short PET_GUARD_MODE = 5;
        public const short const_1914 = 15;
        public const short JUMP = 0;
        public const short ACTIVATE_LASER = 2;
        public const short TOGGLE_WINDOWS = 9;
        public const short QUICKSLOT = 7;
        public const short ZOOM_IN = 11;
        public const short LAUNCH_ROCKET = 3;
        public const short CHANGE_CONFIG = 1;
        public const short PERFORMANCE_MONITORING = 10;
        public short ID { get; set; } = 8383;
        public short charCode = 0;
        public List<int> keyCodes;
        public int parameter = 0;
        public short actionType = 0;

        public UserKeyBindingsModule(short param1 = 0, List<int> param2 = null, int param3 = 0, short param4 = 0) {
            this.actionType = param1;
            if (param2 == null) {
                this.keyCodes = new List<int>();
            } else {
                this.keyCodes = param2;
            }
            this.parameter = param3;
            this.charCode = param4;
        }

        public void Read(IDataInput param1, ICommandLookup lookup) {
            this.charCode = param1.ReadShort();
            this.keyCodes.Clear();
            for (int i = param1.ReadInt(); i > 0; i--) {
                var tmp_0 = param1.Shift(param1.ReadInt(), 30);
                this.keyCodes.Add(tmp_0);
            }
            this.parameter = param1.ReadInt();
            this.parameter = param1.Shift(this.parameter, 5);
            this.actionType = param1.ReadShort();
        }

        public void Write(IDataOutput param1) {
            param1.WriteShort(ID);
            this.method_9(param1);
        }

        protected void method_9(IDataOutput param1) {
            param1.WriteShort(this.charCode);
            param1.WriteInt(this.keyCodes.Count);
            foreach (var tmp_0 in this.keyCodes) {
                param1.WriteInt(param1.Shift(tmp_0, 2));
            }
            param1.WriteInt(param1.Shift(this.parameter, 27));
            param1.WriteShort(this.actionType);
        }
    }
}
