using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class RocketAmmunition : ItemBase {

        #region {[ STATIC ]}
        public static RocketAmmunition R310 { get; } = new RocketAmmunition(0, "ammunition_rocket_r-310", 1000);
        public static RocketAmmunition PLT_2026 { get; } = new RocketAmmunition(2, "ammunition_rocket_plt-2026", 2000);
        public static RocketAmmunition PLT_2021 { get; } = new RocketAmmunition(3, "ammunition_rocket_plt-2021", 4000);
        public static RocketAmmunition PLT_3030 { get; } = new RocketAmmunition(4, "ammunition_rocket_plt-3030", 6000);


        public static RocketAmmunition PLD_8 { get; } = new RocketAmmunition(5, "ammunition_specialammo_pld-8", 0);
        public static RocketAmmunition DCR_250 { get; } = new RocketAmmunition(8, "ammunition_specialammo_dcr-250", 0);
        public static RocketAmmunition R_IC3 { get; } = new RocketAmmunition(9, "ammunition_specialammo_r-ic3", 0);
        #endregion

        #region {[ PROPERTIES ]}
        public int Damage { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private RocketAmmunition(int id, string name, int damage) {
            ID = id;
            Damage = damage;
            Name = name;
        }
        #endregion

    }
}
