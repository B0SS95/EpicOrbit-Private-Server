using EpicOrbit.Shared.Items.Abstracts;
using System;

namespace EpicOrbit.Shared.Items {
    public sealed class ShipAbility : ItemBase {

        #region {[ STATIC ]}
        public static ShipAbility SINGULARITY { get; } = new ShipAbility(1, "ability_venom", TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(180));
        public static ShipAbility FORTRESS { get; } = new ShipAbility(2, "ability_sentinel", TimeSpan.FromSeconds(35), TimeSpan.FromSeconds(180));
        public static ShipAbility PRISMATIC_SHIELD { get; } = new ShipAbility(3, "ability_spectrum", TimeSpan.FromSeconds(22), TimeSpan.FromSeconds(180));
        public static ShipAbility AFTERBURNER { get; } = new ShipAbility(4, "ability_lightning", TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(180));
        public static ShipAbility WEAKEN_SHIELDS { get; } = new ShipAbility(5, "ability_diminisher", TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(30));
        public static ShipAbility NANO_CLUSTER_REPAIR { get; } = new ShipAbility(6, "ability_solace", TimeSpan.Zero, TimeSpan.FromSeconds(180));
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
        private ShipAbility(int id, string name, TimeSpan duration, TimeSpan cooldown) {
            ID = id;
            Duration = duration;
            Cooldown = cooldown;
            Name = name;
        }
        #endregion

    }
}
