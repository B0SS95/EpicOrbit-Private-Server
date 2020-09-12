using EpicOrbit.Shared.Items.Abstracts;
using System;

namespace EpicOrbit.Shared.Items {
    public sealed class RocketLauncherAmmunition : ItemBase {

        #region {[ STATIC ]}
        public static TimeSpan Cooldown = TimeSpan.FromSeconds(3);

        public static RocketLauncherAmmunition HSTRM_01 { get; } = new RocketLauncherAmmunition(21, "ammunition_rocketlauncher_hstrm-01", 4000, 0);
        public static RocketLauncherAmmunition UBR_100 { get; } = new RocketLauncherAmmunition(22, "ammunition_rocketlauncher_ubr-100", 7500, 0); // +80% wenn npc gegner ist
        public static RocketLauncherAmmunition ECO_10 { get; } = new RocketLauncherAmmunition(23, "ammunition_rocketlauncher_eco-10", 2000, 0);
        public static RocketLauncherAmmunition SAR_01 { get; } = new RocketLauncherAmmunition(24, "ammunition_rocketlauncher_sar-01", 0, 1000);
        public static RocketLauncherAmmunition SAR_02 { get; } = new RocketLauncherAmmunition(25, "ammunition_rocketlauncher_sar-02", 0, 4000);
        public static RocketLauncherAmmunition CBR { get; } = new RocketLauncherAmmunition(39, "ammunition_rocketlauncher_cbr", 3000, 3000);
        #endregion

        #region {[ PROPERTIES ]}
        public int Damage { get; }
        public int ShieldDamage { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private RocketLauncherAmmunition(int id, string name, int damage, int shieldDamage) {
            ID = id;
            Damage = damage;
            ShieldDamage = shieldDamage;
            Name = name;
        }
        #endregion

    }
}
