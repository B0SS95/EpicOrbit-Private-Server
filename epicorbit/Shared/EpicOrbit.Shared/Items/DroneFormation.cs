using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.Items.Abstracts;
using EpicOrbit.Shared.ViewModels.Boost;

namespace EpicOrbit.Shared.Items {
    public sealed class DroneFormation : ItemBase {

        #region {[ STATIC ]}
        public static DroneFormation DEFAULT { get; } = new DroneFormation(0, "drone_formation_default", new BoostView[0]);

        public static DroneFormation TURTLE { get; } = new DroneFormation(1, "drone_formation_f-01-tu", new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1),
            new BoostView(BoosterType.DAMAGE_ROCKETS, 0.925)
        });

        public static DroneFormation ARROW { get; } = new DroneFormation(2, "drone_formation_f-02-ar", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_ROCKETS, 1.3),
            new BoostView(BoosterType.DAMAGE_LASER, 0.97)
        });

        public static DroneFormation LANCE { get; } = new DroneFormation(3, "drone_formation_f-03-la", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_MINE, 1.5)
        });

        public static DroneFormation STAR { get; } = new DroneFormation(4, "drone_formation_f-04-st", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_ROCKETS, 1.25),
            new BoostView(BoosterType.MISS_RATE, 1.55), // simply subtract this value from the opponents hit rate
            new BoostView(BoosterType.ROCKET_LAUNCHER_COOLDOWN, 1.33) // this is negative lol
        });

        public static DroneFormation PINCER { get; } = new DroneFormation(5, "drone_formation_f-05-pi", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVP, 1.03),
            new BoostView(BoosterType.HONOR, 1.05),
            new BoostView(BoosterType.SHIELD_ABSORBATION, 0.9)
        });

        public static DroneFormation DOUBLE_ARROW { get; } = new DroneFormation(6, "drone_formation_f-06-da", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_ROCKETS, 1.3),
            new BoostView(BoosterType.LASER_SHIELD_PENETRATION, 1.1),
            new BoostView(BoosterType.SHIELD, 0.8)
        });

        public const int DIAMOND_ID = 7; // for switch
        public static DroneFormation DIAMOND { get; } = new DroneFormation(DIAMOND_ID, "drone_formation_f-07-di", new BoostView[] {
            new BoostView(BoosterType.HITPOINTS, 0.7)
        }); // +1% Schild/s

        public static DroneFormation CHEVRON { get; } = new DroneFormation(8, "drone_formation_f-08-ch", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_ROCKETS, 1.65),
            new BoostView(BoosterType.HITPOINTS, 0.8)
        });

        public const int MOTH_ID = 9; // for switch
        public static DroneFormation MOTH { get; } = new DroneFormation(MOTH_ID, "drone_formation_f-09-mo", new BoostView[] {
            new BoostView(BoosterType.HITPOINTS, 1.2),
            new BoostView(BoosterType.LASER_SHIELD_PENETRATION, 1.2)
        }); // -5% Schild/s

        public static DroneFormation CRAB { get; } = new DroneFormation(10, "drone_formation_f-10-cr", new BoostView[] {
            new BoostView(BoosterType.SHIELD_ABSORBATION, 1.2),
            new BoostView(BoosterType.SPEED, 0.85)
        });

        public static DroneFormation HEART { get; } = new DroneFormation(11, "drone_formation_f-11-he", new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.2),
            new BoostView(BoosterType.HITPOINTS, 1.2),
            new BoostView(BoosterType.DAMAGE_LASER, 0.95)
        });

        public static DroneFormation BARRAGE { get; } = new DroneFormation(12, "drone_formation_f-12-ba", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVE, 1.05),
            new BoostView(BoosterType.EXPERIENCE, 1.05),
            new BoostView(BoosterType.SHIELD_ABSORBATION, 0.85)
        });

        public static DroneFormation BAT { get; } = new DroneFormation(13, "drone_formation_f-13-bt", new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVE, 1.08),
            new BoostView(BoosterType.EXPERIENCE, 1.08),
            new BoostView(BoosterType.SPEED, 0.85)
        });

        public static DroneFormation RING { get; } = new DroneFormation(14, "drone_formation_f-3d-rg", new BoostView[] {
               new BoostView(BoosterType.SHIELD, 1.85),
               new BoostView(BoosterType.DAMAGE_LASER, 0.75),
               new BoostView(BoosterType.ROCKET_LAUNCHER_COOLDOWN, 1.25),
               new BoostView(BoosterType.ROCKET_COOLDOWN, 1.25),
               new BoostView(BoosterType.SPEED, 0.95)
           });

        public static DroneFormation DRILL { get; } = new DroneFormation(15, "drone_formation_f-3d-dr", new BoostView[] {
               new BoostView(BoosterType.DAMAGE_LASER, 1.2),
               new BoostView(BoosterType.SHIELD, 0.75),
               new BoostView(BoosterType.SHIELD_ABSORBATION, 0.95),
               new BoostView(BoosterType.SPEED, 0.95)
           });

        public static DroneFormation VETERAN { get; } = new DroneFormation(16, "drone_formation_f-3d-vt", new BoostView[] {
               new BoostView(BoosterType.HONOR, 1.2),
               new BoostView(BoosterType.DAMAGE_LASER, 0.80),
               new BoostView(BoosterType.SHIELD, 0.80),
               new BoostView(BoosterType.HITPOINTS, 0.80)
           });

        public const int DOME_ID = 17; // for switch
        public static DroneFormation DOME { get; } = new DroneFormation(DOME_ID, "drone_formation_f-3d-dm", new BoostView[] {
               new BoostView(BoosterType.SHIELD, 1.4),
               new BoostView(BoosterType.DAMAGE_LASER, 0.70),
               new BoostView(BoosterType.SPEED, 0.85),
               new BoostView(BoosterType.ROCKET_COOLDOWN, 1.25),
               new BoostView(BoosterType.ROCKET_LAUNCHER_COOLDOWN, 1.25)
           }); // +1.5% Schild/s

        public const int WHEEL_ID = 18; // for switch
        public static DroneFormation WHEEL { get; } = new DroneFormation(18, "drone_formation_f-3d-wl", new BoostView[] {
               new BoostView(BoosterType.SPEED, 1.05),
               new BoostView(BoosterType.DAMAGE_LASER, 0.80)
           });

        public static DroneFormation X { get; } = new DroneFormation(19, "drone_formation_f-3d-x", new BoostView[] {
               new BoostView(BoosterType.HONOR, 0.00000000001), // -100%
               new BoostView(BoosterType.DAMAGE_LASER_PVP, 0.00000000001), // -100%
               new BoostView(BoosterType.DAMAGE_LASER_PVE, 1.05),
               new BoostView(BoosterType.EXPERIENCE_PVE, 1.05),
               new BoostView(BoosterType.HITPOINTS, 1.08)
           });

        public static DroneFormation WAVE { get; } = new DroneFormation(20, "drone_formation_f-3d-wv", new BoostView[0]);


        #endregion

        #region {[ PROPERTIES ]}
        public BoostView[] Stats { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private DroneFormation(int id, string name, BoostView[] stats) {
            ID = id;
            Name = name;
            Stats = stats;
        }
        #endregion

    }
}
