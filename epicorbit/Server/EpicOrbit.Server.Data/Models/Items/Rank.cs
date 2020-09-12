using EpicOrbit.Shared.Items.Interfaces;

namespace EpicOrbit.Server.Data.Models.Items {
    public sealed class Rank : IIndentifyable {

        #region {[ STATIC ]}
        public static Rank BasicSpacePilot { get; } = new Rank(1, 0.2);
        public static Rank SpacePilot { get; } = new Rank(2, 0.1239);
        public static Rank ChiefSpacePilot { get; } = new Rank(3, 0.1);
        public static Rank BasicSergeant { get; } = new Rank(4, 0.09);
        public static Rank Sergeant { get; } = new Rank(5, 0.08);
        public static Rank ChiefSergeant { get; } = new Rank(6, 0.07);
        public static Rank BasicLieutenant { get; } = new Rank(7, 0.06);
        public static Rank Lieutenant { get; } = new Rank(8, 0.05);
        public static Rank ChiefLieutenant { get; } = new Rank(9, 0.045);
        public static Rank BasicCaptain { get; } = new Rank(10, 0.04);
        public static Rank Captain { get; } = new Rank(11, 0.035);
        public static Rank ChiefCaptain { get; } = new Rank(12, 0.03);
        public static Rank BasicMajor { get; } = new Rank(13, 0.025);
        public static Rank Major { get; } = new Rank(14, 0.02);
        public static Rank ChiefMajor { get; } = new Rank(15, 0.015);
        public static Rank BasicColonel { get; } = new Rank(16, 0.01);
        public static Rank Colonel { get; } = new Rank(17, 0.005);
        public static Rank ChiefColonel { get; } = new Rank(18, 0.001);
        public static Rank BasicGeneral { get; } = new Rank(19, 0.0001);
        public static Rank General { get; } = new Rank(20, 0);
        public static Rank Administrator { get; } = new Rank(21, 0);
        public static Rank Wanted { get; } = new Rank(22, 0);
        #endregion

        #region {[ PROPERTIES ]}
        public int ID { get; }
        public bool IsSpecial { get; }
        public double Propabilty { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Rank(int id, double propability = 0) {
            ID = id;
            Propabilty = propability;
            if (propability <= 0) {
                IsSpecial = true;
            }
        }
        #endregion

    }
}
