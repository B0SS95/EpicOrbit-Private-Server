using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Weapon : ItemBase {

        #region {[ STATIC ]}
        public static Weapon LF1 { get; } = new Weapon(1, "equipment_weapon_laser_lf-1", 40);
        public static Weapon MP1 { get; } = new Weapon(2, "equipment_weapon_laser_mp-1", 60);
        public static Weapon LF2 { get; } = new Weapon(3, "equipment_weapon_laser_lf-2", 100);
        public static Weapon LF3 { get; } = new Weapon(4, "equipment_weapon_laser_lf-3", 150);
        public static Weapon LF4 { get; } = new Weapon(5, "equipment_weapon_laser_lf-4", 200);
        #endregion

        #region {[ PROPERTIES ]}
        public int Damage { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public Weapon(int id, string name, int damage) {
            ID = id;
            Name = name;
            Damage = damage;
        }
        #endregion

    }
}
