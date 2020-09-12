using EpicOrbit.Shared.Items.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpicOrbit.Shared.Items.Extensions {
    public static class ItemsExtension {

        public static Drone FromDrones(this int id) {
            return ItemsExtension<Drone>.Lookup(id);
        }

        public static DroneDesign FromDroneDesigns(this int id) {
            return ItemsExtension<DroneDesign>.Lookup(id);
        }

        public static DroneFormation FromDroneFormations(this int id) {
            return ItemsExtension<DroneFormation>.Lookup(id);
        }

        public static Ship FromShips(this int id) {
            return ItemsExtension<Ship>.Lookup(id);
        }

        public static Weapon FromWeapons(this int id) {
            return ItemsExtension<Weapon>.Lookup(id);
        }

        public static Shield FromShields(this int id) {
            return ItemsExtension<Shield>.Lookup(id);
        }

        public static Generator FromGenerators(this int id) {
            return ItemsExtension<Generator>.Lookup(id);
        }

        public static Faction FromFactions(this int id) {
            return ItemsExtension<Faction>.Lookup(id);
        }

        public static Ammuninition FromAmmunitions(this int id) {
            return ItemsExtension<Ammuninition>.Lookup(id);
        }

        public static RocketAmmunition FromRocketAmmunitions(this int id) {
            return ItemsExtension<RocketAmmunition>.Lookup(id);
        }

        public static RocketLauncherAmmunition FromRocketLauncherAmmunitions(this int id) {
            return ItemsExtension<RocketLauncherAmmunition>.Lookup(id);
        }

    }

    public static class ItemsExtension<T> where T : IIndentifyable {

        public static Dictionary<int, T> Items => _lookup;

        private static Dictionary<int, T> _lookup;
        static ItemsExtension() {
            _lookup = typeof(T)
                   .GetProperties(BindingFlags.Public | BindingFlags.Static)
                   .Where(x => x.PropertyType == typeof(T))
                   .Select(x => (T)x.GetValue(null))
                   .ToDictionary(x => x.ID, x => x);
        }

        public static T Lookup(int id) {
            if (_lookup == null) {
                _lookup = typeof(T)
                    .GetProperties(BindingFlags.Public | BindingFlags.Static)
                    .Where(x => x.PropertyType == typeof(T))
                    .Select(x => (T)x.GetValue(null))
                    .ToDictionary(x => x.ID, x => x);
            }

            if (!_lookup.TryGetValue(id, out T result)) {
                throw new KeyNotFoundException($"Could not find the corresponding object for id: {id}");
            }

            return result;
        }

    }
}
