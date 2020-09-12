using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Enumerables;
using EpicOrbit.Shared.Items;
using EpicOrbit.Server.Data.Models.Modules;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class NpcHangarAssembly : HangarAssembly {

        #region {[ PROPERTIES ]}
        protected override int _maxHitpoints => _hp;
        protected override int _maxShield => _shd;
        #endregion

        #region {[ FIELDS ]}
        private int _hp;
        private int _shd;
        #endregion

        public NpcHangarAssembly(EntityControllerBase controller, Ship ship, Map map, Position position, int hitpoints, int shield) : base(controller) {
            Ship = ship;
            Map = map;
            Position = position;

            _hp = hitpoints;
            _shd = shield;

            Shield = MaxShield;
            Hitpoints = MaxHitpoints;
        }

    }
}
