using System;
using System.Collections.Generic;
using System.Text;

namespace EpicOrbit.Server.Data.Models.Modules {
    public class Position {

        #region {[ PROPERTIES ]}
        public int X { get; set; }

        public int Y { get; set; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public Position() { }
        public Position(int x, int y) {
            X = x;
            Y = y;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public double DistanceTo(Position point) {
            long dx = point.X - X;
            long dy = point.Y - Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public bool Same(Position point) {
            return X == point.X && Y == point.Y;
        }

        public override string ToString() {
            return "{ X: " + X + ", Y:" + Y + "}";
        }
        #endregion

    }
}
