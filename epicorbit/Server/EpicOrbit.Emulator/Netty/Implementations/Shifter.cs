using EpicOrbit.Emulator.Netty.Interfaces;

namespace EpicOrbit.Emulator.Netty.Implementations {
    internal class Shifter : IShiftable {

        public int Shift(int value, int amount) {
            return (int)(((uint)value << 32 - amount) | ((uint)value >> amount));
        }

    }
}
