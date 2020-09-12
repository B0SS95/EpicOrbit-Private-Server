using EpicOrbit.Shared.Items.Abstracts;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpicOrbit.Emulator.Game.Controllers.Assemblies.ItemSelection.Abstracts {
    public abstract class ItemSelectionHandlerBase<T> where T : ItemBase {

        #region {[ STATIC ]}
        private static Dictionary<string, T> GetAll() {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.PropertyType == typeof(T))
                .Select(x => (T)x.GetValue(null))
                .ToDictionary(x => x.Name, x => x);
        }
        #endregion

        #region {[ PROPERTIES ]}
        public List<T> Items { get; }
        public Dictionary<string, T> Lookup { get; }
        #endregion

        #region {[ FIELDS ]}
        protected List<string> _items;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ItemSelectionHandlerBase() {
            Lookup = GetAll();
            _items = Lookup.Select(x => x.Key).ToList();
            Items = Lookup.Select(x => x.Value).ToList();
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public bool Contains(string itemId) {
            return Lookup.ContainsKey(itemId);
        }

        public abstract void Handle(PlayerController playerController, string itemId, bool initAttack);
        #endregion

    }
}
