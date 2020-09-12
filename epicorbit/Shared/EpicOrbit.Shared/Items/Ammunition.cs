using EpicOrbit.Shared.Items.Abstracts;
using EpicOrbit.Shared.Items.Interfaces;

namespace EpicOrbit.Shared.Items {

    public sealed class Ammuninition : ItemBase {

        #region {[ STATIC ]}
        public static Ammuninition LCB_10 { get; } = new Ammuninition(0, "ammunition_laser_lcb-10", 1, 0);
        public static Ammuninition MCB_25 { get; } = new Ammuninition(1, "ammunition_laser_mcb-25", 2, 0);
        public static Ammuninition MCB_50 { get; } = new Ammuninition(2, "ammunition_laser_mcb-50", 3, 0);
        public static Ammuninition UCB_100 { get; } = new Ammuninition(3, "ammunition_laser_ucb-100", 4, 0);
        public static Ammuninition SAB_50 { get; } = new Ammuninition(4, "ammunition_laser_sab-50", 0, 2);
        public static Ammuninition CBO_100 { get; } = new Ammuninition(8, "ammunition_laser_cbo-100", 3, 1);
        public static Ammuninition RSB_75 { get; } = new Ammuninition(6, "ammunition_laser_rsb-75", 6, 0);
        public static Ammuninition PIB_100 { get; } = new Ammuninition(109, "ammunition_laser_pib-100", 4, 0);
        #endregion

        #region {[ PROPERTIES ]}
        public int DamageMultiplier { get; }
        public int ShieldMultiplier { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Ammuninition(int id, string name, int damageMulitplier, int shieldMultiplier) {
            ID = id;

            DamageMultiplier = damageMulitplier;
            ShieldMultiplier = shieldMultiplier;

            Name = name;
        }
        #endregion

    }
}
