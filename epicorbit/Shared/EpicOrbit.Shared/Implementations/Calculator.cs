using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Shared.Implementations {
    public static class Calculator { // TODO: Move to extensions

        public static int Level(this long experience) {
            if (experience < 10000) {
                return 1;
            }

            double result = (-(Math.Log(10000.0 / experience) / Math.Log(2))) + 2;
            return (int)result; //nachkomma abschneiden
        }

        public static long Experience(this int level) {
            return (long)(10000 * Math.Pow(2, level - 1));
        }

    }
}
