using EpicOrbit.Shared.Enumerables;
using EpicOrbit.Shared.ViewModels.Boost;
using EpicOrbit.Shared.Items.Abstracts;

namespace EpicOrbit.Shared.Items {
    public sealed class Ship : ItemBase {

        #region {[ STATIC ]}
        public static Ship PHOENIX { get; } = new Ship(100, "ship_phoenix", "Phoenix", 4000, 320, 100, 1, 1, 100, 1, 1);

        public static Ship YAMATO { get; } = new Ship(200, "ship_yamato", "Yamato", 8000, 340, 200, 2, 2, 200, 2, 2);

        public static Ship LEONOV { get; } = new Ship(300, "ship_leonov", "Leonov", 64000, 360, 400, 6, 6, 400, 4, 4);

        //public static Ship DEFCOM { get; } = new Ship(400, "ship_defcom", "Defcom", 16000, 340, 800, 3, 5, 400, 4, 4, )
        #region {[ GOLIATH ]}
        public static Ship GOLIATH { get; } = new Ship(1000, "ship_goliath", "Goliath", 256000, 300, 1500, 15, 15, 51200, 512, 20);

        public static Ship GOLIATH_DIMINISHER { get; } = new Ship(1001, "ship_diminisher", "Goliath Diminisher", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.WEAKEN_SHIELDS
        });

        public static Ship GOLIATH_SENTINEL { get; } = new Ship(1002, "ship_sentinel", "Goliath Sentinel", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SOLACE { get; } = new Ship(1003, "ship_solace", "Goliath Solace", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SPECTRUM { get; } = new Ship(1004, "ship_spectrum", "Goliath Spectrum", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_VENOM { get; } = new Ship(1005, "ship_venom", "Goliath Venom", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_BASTION { get; } = new Ship(1006, "ship_g-bastion", "Goliath Bastion", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        });

        public static Ship GOLIATH_CENTAUR { get; } = new Ship(1007, "ship_g-centaur", "Goliath Centaur", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.HITPOINTS, 1.1)
        });

        public static Ship GOLIATH_CHAMPION { get; } = new Ship(1008, "ship_g-champion", "Goliath Champion", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05),
            new BoostView(BoosterType.HONOR, 1.1)
        });

        public static Ship GOLIATH_ENFORCER { get; } = new Ship(1009, "ship_g-enforcer", "Goliath Enforcer", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        });

        public static Ship GOLIATH_EXALTED { get; } = new Ship(1010, "ship_g-exalted", "Goliath Exalted", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.HONOR, 1.1)
        });

        public static Ship GOLIATH_GOAL { get; } = new Ship(1011, "ship_g-goal", "Goliath Goal", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.EXPERIENCE, 1.1)
        });

        public static Ship GOLIATH_IGNITE { get; } = new Ship(1012, "ship_g-ignite", "Goliath Ignite", 256000, 300, 1500, 15, 15, 51200, 512, 20);

        public static Ship GOLIATH_KICK { get; } = new Ship(1013, "ship_g-kick", "Goliath Kick", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        });

        public static Ship GOLIATH_X { get; } = new Ship(1014, "ship_goliath-x", "Goliath X", 256000, 300, 1500, 15, 15, 51200, 512, 20);

        public static Ship GOLIATH_PEACEMAKER { get; } = new Ship(1015, "ship_g-peacemaker", "Goliath Peacermaker", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVP, 1.07),
            new BoostView(BoosterType.HONOR, 1.05)
        });

        public static Ship GOLIATH_REFEREE { get; } = new Ship(1016, "ship_g-referee", "Goliath Referee", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        });

        public static Ship GOLIATH_SATURN { get; } = new Ship(1017, "ship_g-saturn", "Goliath Saturn", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.HITPOINTS, 1.2)
        });

        public static Ship GOLIATH_SOVEREIGN { get; } = new Ship(1018, "ship_g-sovereign", "Goliath Sovereign", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVP, 1.07),
            new BoostView(BoosterType.HONOR, 1.05)
        });

        public static Ship GOLIATH_SURGEON { get; } = new Ship(1019, "ship_g-surgeon", "Goliath Surgeon", 256000, 300, 1500, 15, 16, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.06),
            new BoostView(BoosterType.EXPERIENCE, 1.06),
            new BoostView(BoosterType.HONOR, 1.06)
        });

        public static Ship GOLIATH_VANQUISHER { get; } = new Ship(1020, "ship_g-vanquisher", "Goliath Vanquisher", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE_LASER_PVP, 1.07),
            new BoostView(BoosterType.HONOR, 1.05)
        });

        public static Ship GOLIATH_VETERAN { get; } = new Ship(1021, "ship_g-veteran", "Goliath Veteran", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.EXPERIENCE, 1.1)
        });

        public static Ship GOLIATH_DIMINISHER_ARGON { get; } = new Ship(1022, "ship_diminisher_design_diminisher-argon", "Diminisher Argon", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.WEAKEN_SHIELDS
        });

        public static Ship GOLIATH_DIMINISHER_EXPO2016 { get; } = new Ship(1023, "ship_diminisher_design_diminisher-expo2016", "Diminisher EXPO2016", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.WEAKEN_SHIELDS
        });

        public static Ship GOLIATH_DIMINISHER_LAVA { get; } = new Ship(1024, "ship_diminisher_design_diminisher-lava", "Diminisher Lava", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.WEAKEN_SHIELDS
        });

        public static Ship GOLIATH_DIMINISHER_LEGEND { get; } = new Ship(1025, "ship_diminisher_design_diminisher-legend", "Diminisher Legend", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.WEAKEN_SHIELDS
        });

        public static Ship GOLIATH_SENTINEL_ARGON { get; } = new Ship(1026, "ship_sentinel_design_sentinel-argon", "Sentinel Argon", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SENTINEL_EXPO2016 { get; } = new Ship(1027, "ship_sentinel_design_sentinel-expo2016", "Sentinel EXPO2016", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SENTINEL_FROST { get; } = new Ship(1028, "ship_sentinel_design_sentinel-frost", "Sentinel Frost", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SENTINEL_INFERNO { get; } = new Ship(1029, "ship_sentinel_design_sentinel-inferno", "Sentinel Inferno", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SENTINEL_LAVA { get; } = new Ship(1030, "ship_sentinel_design_sentinel-lava", "Sentinel Lava", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SENTINEL_LEGEND { get; } = new Ship(1031, "ship_sentinel_design_sentinel-legend", "Sentinel Legend", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.FORTRESS
        });

        public static Ship GOLIATH_SOLACE_ARGON { get; } = new Ship(1032, "ship_solace_design_solace-argon", "Solace Argon", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SOLACE_BLAZE { get; } = new Ship(1033, "ship_solace_design_solace-blaze", "Solace Blaze", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SOLACE_BOREALIS { get; } = new Ship(1034, "ship_solace_design_solace-borealis", "Solace Borealis", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SOLACE_OCEAN { get; } = new Ship(1035, "ship_solace_design_solace-ocean", "Solace Ocean", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SOLACE_POISON { get; } = new Ship(1036, "ship_solace_design_solace-poison", "Solace Poison", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.NANO_CLUSTER_REPAIR
        });

        public static Ship GOLIATH_SPECTRUM_BLAZE { get; } = new Ship(1037, "ship_spectrum_design_spectrum-blaze", "Spectrum Blaze", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_FROST { get; } = new Ship(1038, "ship_spectrum_design_spectrum-frost", "Spectrum Frost", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_INFERNO { get; } = new Ship(1039, "ship_spectrum_design_spectrum-inferno", "Spectrum Inferno", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_LAVA { get; } = new Ship(1040, "ship_spectrum_design_spectrum-lava", "Spectrum Lava", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_LEGEND { get; } = new Ship(1041, "ship_spectrum_design_spectrum-legend", "Spectrum Legend", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_OCEAN { get; } = new Ship(1042, "ship_spectrum_design_spectrum-ocean", "Spectrum Ocean", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_POISON { get; } = new Ship(1043, "ship_spectrum_design_spectrum-poison", "Spectrum Poison", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_SPECTRUM_SANDSTORM { get; } = new Ship(1044, "ship_spectrum_design_spectrum-sandstorm", "Spectrum Sandstorm", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.SHIELD, 1.1)
        }, new ShipAbility[] {
            ShipAbility.PRISMATIC_SHIELD
        });

        public static Ship GOLIATH_VENOM_ARGON { get; } = new Ship(1045, "ship_venom_design_venom-argon", "Venom Argon", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_BLAZE { get; } = new Ship(1046, "ship_venom_design_venom-blaze", "Venom Blaze", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_BOREALIS { get; } = new Ship(1047, "ship_venom_design_venom-borealis", "Venom Borealis", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_FROST { get; } = new Ship(1048, "ship_venom_design_venom-frost", "Venom Frost", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_INFERNO { get; } = new Ship(1049, "ship_venom_design_venom-inferno", "Venom Inferno", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_OCEAN { get; } = new Ship(1050, "ship_venom_design_venom-ocean", "Venom Ocean", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship GOLIATH_VENOM_POISON { get; } = new Ship(1051, "ship_venom_design_venom-poison", "Venom Poison", 256000, 300, 1500, 15, 15, 51200, 512, 20, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.05)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });
        #endregion

        #region {[ CYBORG ]}
        public static Ship CYBORG { get; } = new Ship(1100, "ship_cyborg", "Cyborg", 265000, 300, 1500, 16, 16, 102400, 1024, 40, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.1)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship CYBORG_CARBONITE { get; } = new Ship(1101, "ship_cyborg_design_cyborg-carbonite", "Cyborg Carbonite", 265000, 300, 1500, 16, 16, 102400, 1024, 40, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.1)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });

        public static Ship CYBORG_LAVA { get; } = new Ship(1102, "ship_cyborg_design_cyborg-lava", "Cyborg Lava", 265000, 300, 1500, 16, 16, 102400, 1024, 40, 1, new BoostView[] {
            new BoostView(BoosterType.DAMAGE, 1.1)
        }, new ShipAbility[] {
            ShipAbility.SINGULARITY
        });
        #endregion

        #endregion

        #region {[ PROPERTIES ]}
        public string Description { get; }
        public int Expansionstage { get; }
        public int ShipType { get; }
        public int Hitpoints { get; }
        public int Speed { get; }
        public int Cargo { get; }
        public int RocketLauncherSlots { get; }
        public int WeaponSlots { get; }
        public int GeneratorSlots { get; }
        public int Experience { get; }
        public int Honor { get; }
        public int Rankpoints { get; }
        public BoostView[] Boosts { get; }
        public ShipAbility[] Abilities { get; }
        #endregion

        #region {[ ItemBase implementation ]} 
        public override int ID { get; }
        public override string Name { get; }
        #endregion

        #region {[ CONSTRUCTOR ]}
        public Ship(int id, string name, string description, int hitpoints, int speed, int cargo, int weaponSlots, int generatorSlots, int experience, int honor, int rankpoints, int rlSlots = 1, BoostView[] boosts = null, ShipAbility[] abilities = null) {
            ID = id;
            Hitpoints = hitpoints;
            Speed = speed;
            Cargo = cargo;
            WeaponSlots = weaponSlots;
            GeneratorSlots = generatorSlots;
            Experience = experience;
            Honor = honor;
            Rankpoints = rankpoints;
            RocketLauncherSlots = rlSlots;

            Name = name;
            Description = description;

            Boosts = boosts ?? new BoostView[0];
            Abilities = abilities ?? new ShipAbility[0];
            Expansionstage = 10;
        }
        #endregion

    }
}
