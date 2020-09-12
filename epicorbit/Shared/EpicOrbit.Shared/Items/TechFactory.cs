using EpicOrbit.Shared.Items.Abstracts;
using System;

namespace EpicOrbit.Shared.Items {
    public sealed class TechFactory : ItemBase {

        #region {[ STATIC ]}
        public static TechFactory ENERGY_LEECH { get; } = new TechFactory(1, "tech_energy-leech", TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(15));
        public static TechFactory CHAIN_IMPULSE { get; } = new TechFactory(2, "tech_chain-impulse", TimeSpan.Zero, TimeSpan.FromMinutes(1));
        public static TechFactory PRECISION_TARGETER { get; } = new TechFactory(3, "tech_precision-targeter", TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(5));
        public static TechFactory SHIELD_BACKUP { get; } = new TechFactory(4, "tech_backup-shields", TimeSpan.Zero, TimeSpan.FromMinutes(2));
        public static TechFactory BATTLE_REPAIR_BOT { get; } = new TechFactory(5, "tech_battle-repair-bot", TimeSpan.FromSeconds(10), TimeSpan.FromMinutes(2));
        #endregion

        #region {[ PROPERTIES ]}
        public TimeSpan Duration { get; }
        public TimeSpan Cooldown { get; }
        #endregion

        #region {[ ItemBase implementation ]}
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        private TechFactory(int id, string name, TimeSpan duration, TimeSpan cooldown) {
            ID = id;

            Duration = duration;
            Cooldown = cooldown;

            Name = name;
        }
        #endregion

    }
}
