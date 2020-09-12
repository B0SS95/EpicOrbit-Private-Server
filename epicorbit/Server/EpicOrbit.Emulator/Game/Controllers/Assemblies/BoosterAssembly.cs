using EpicOrbit.Emulator.Game.Controllers.Abstracts;
using EpicOrbit.Emulator.Game.Controllers.Assemblies.Abstracts;
using System.Collections.Generic;
using EpicOrbit.Shared.Enumerables;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies {
    public class BoosterAssembly : AssemblyBase {

        public delegate void BoostChanged(BoosterType boosterType, double newValue);
        public event BoostChanged OnBoostChanged;

        private Dictionary<BoosterType, double> _boosts;
        private object _lock;

        public BoosterAssembly(EntityControllerBase controller) : base(controller) {
            _boosts = new Dictionary<BoosterType, double>();
            _lock = new object();
        }

        public double Get(BoosterType boosterType) {
            lock (_lock) {
                if (_boosts.TryGetValue(boosterType, out double boost)) {
                    return boost;
                }
                return 1.0;
            }
        }

        public void Multiply(BoosterType boosterType, double percent) {
            lock (_lock) {
                Set(boosterType, Get(boosterType) * percent);
            }
        }

        public void Divide(BoosterType boosterType, double percent) {
            lock (_lock) {
                Set(boosterType, Get(boosterType) / percent);
            }
        }

        public void Set(BoosterType boosterType, double boost) {
            lock (_lock) {
                _boosts[boosterType] = boost;
                OnBoostChanged?.Invoke(boosterType, boost);
            }
        }

        public override void Refresh() { }

    }
}
