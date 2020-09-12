using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Drone : ItemBase {

        #region {[ STATIC ]}
        public static Drone FLAX_LEVEL_1 { get; } = new Drone(11, "drone_flax", 1, 1, 0, 1.06, 1.15);
        public static Drone FLAX_LEVEL_2 { get; } = new Drone(12, "drone_flax", 1, 2, 100, 1.08, 1.19);
        public static Drone FLAX_LEVEL_3 { get; } = new Drone(13, "drone_flax", 1, 3, 200, 1.1, 1.23);
        public static Drone FLAX_LEVEL_4 { get; } = new Drone(14, "drone_flax", 1, 4, 400, 1.12, 1.27);
        public static Drone FLAX_LEVEL_5 { get; } = new Drone(15, "drone_flax", 1, 5, 800, 1.14, 1.31);
        public static Drone FLAX_LEVEL_6 { get; } = new Drone(16, "drone_flax", 1, 6, 1600, 1.16, 1.35);


        public static Drone IRIS_LEVEL_1 { get; } = new Drone(21, "drone_iris", 2, 1, 0, 1.06, 1.15);
        public static Drone IRIS_LEVEL_2 { get; } = new Drone(22, "drone_iris", 2, 2, 100, 1.08, 1.19);
        public static Drone IRIS_LEVEL_3 { get; } = new Drone(23, "drone_iris", 2, 3, 200, 1.1, 1.23);
        public static Drone IRIS_LEVEL_4 { get; } = new Drone(24, "drone_iris", 2, 4, 400, 1.12, 1.27);
        public static Drone IRIS_LEVEL_5 { get; } = new Drone(25, "drone_iris", 2, 5, 800, 1.14, 1.31);
        public static Drone IRIS_LEVEL_6 { get; } = new Drone(26, "drone_iris", 2, 6, 1600, 1.16, 1.35);

        public static Drone APIS_LEVEL_1 { get; } = new Drone(31, "drone_apis", 2, 1, 0, 1.06, 1.15);
        public static Drone APIS_LEVEL_2 { get; } = new Drone(32, "drone_apis", 2, 2, 100, 1.08, 1.19);
        public static Drone APIS_LEVEL_3 { get; } = new Drone(33, "drone_apis", 2, 3, 200, 1.1, 1.23);
        public static Drone APIS_LEVEL_4 { get; } = new Drone(34, "drone_apis", 2, 4, 400, 1.12, 1.27);
        public static Drone APIS_LEVEL_5 { get; } = new Drone(35, "drone_apis", 2, 5, 800, 1.14, 1.31);
        public static Drone APIS_LEVEL_6 { get; } = new Drone(36, "drone_apis", 2, 6, 1600, 1.16, 1.35);

        public static Drone ZEUS_LEVEL_1 { get; } = new Drone(41, "drone_zeus", 2, 1, 0, 1.06, 1.15);
        public static Drone ZEUS_LEVEL_2 { get; } = new Drone(42, "drone_zeus", 2, 2, 100, 1.08, 1.19);
        public static Drone ZEUS_LEVEL_3 { get; } = new Drone(43, "drone_zeus", 2, 3, 200, 1.1, 1.23);
        public static Drone ZEUS_LEVEL_4 { get; } = new Drone(44, "drone_zeus", 2, 4, 400, 1.12, 1.27);
        public static Drone ZEUS_LEVEL_5 { get; } = new Drone(45, "drone_zeus", 2, 5, 800, 1.14, 1.31);
        public static Drone ZEUS_LEVEL_6 { get; } = new Drone(46, "drone_zeus", 2, 6, 1600, 1.16, 1.35);
        #endregion

        #region {[ PROPERTIES ]}
        public int Slots { get; }
        public int Level { get; }
        public int PointsRequired { get; }
        public double DamageBoost { get; }
        public double ShieldBoost { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private Drone(int id, string name, int slots, int level, int pointsRequired, double damageBoost, double shieldBoost) {
            ID = id;
            Slots = slots;
            Level = level;
            PointsRequired = pointsRequired;
            DamageBoost = damageBoost;
            ShieldBoost = shieldBoost;

            Name = name;
        }
        #endregion

    }
}
