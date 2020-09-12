using System;
using System.Diagnostics;

namespace EpicOrbit.Emulator.Game.Implementations {
    public class OffsetStopwatch : Stopwatch {

        public TimeSpan StartOffset { get; private set; }

        public OffsetStopwatch(TimeSpan startOffset) {
            StartOffset = startOffset;
        }

        public new long ElapsedMilliseconds => base.ElapsedMilliseconds + (long)StartOffset.TotalMilliseconds;

        public new long ElapsedTicks => base.ElapsedTicks + StartOffset.Ticks;

        public double TotalElapsedMilliseconds => base.Elapsed.TotalMilliseconds + StartOffset.TotalMilliseconds;

    }
}
