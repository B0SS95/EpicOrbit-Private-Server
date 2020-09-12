namespace EpicOrbit.Shared.Enumerables {
    public enum BoosterType {

        EXPERIENCE,
        HONOR,
        DAMAGE,
        SHIELD,
        HITPOINTS_REGENERATION,
        SHIELD_REGNERATION,
        RESOURCES,
        HITPOINTS,
        COOLDOWN,
        DAMAGE_ROCKETS,
        DAMAGE_LASER,
        DAMAGE_MINE,
        RADIUS_MINE,
        MISS_RATE, // this is the propability to being missed
        HIT_RATE, // this is the propability to hit ----  this.HIT_RATE * enemy.MISS_RATE = current rate
        ROCKET_LAUNCHER_COOLDOWN,
        DAMAGE_LASER_PVP,
        LASER_SHIELD_PENETRATION,
        SHIELD_ABSORBATION,
        SPEED,
        DAMAGE_LASER_PVE,
        ROCKET_COOLDOWN,
        EXPERIENCE_PVE,
        DAMAGE_LASER_TO_HITPOINTS_TRANSFORMER,
        DAMAGE_SHIELD, // damage on shield
        ENEMY_DAMAGE_LASER // boost on enemy's damage (if < 1 -> reduction)
    }
}
