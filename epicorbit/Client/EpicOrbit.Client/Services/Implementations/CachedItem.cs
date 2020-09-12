using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EpicOrbit.Client.Services.Implementations {
    public class CachedItem<T> where T : class {

        #region {[ DELEGATE ]}
        public delegate bool Fetch(out T item, out string message, out HttpStatusCode code);
        #endregion

        #region {[ FIELDS ]}
        private Fetch _getter;

        private bool _available;
        private T _cache;

        private TimeSpan _timeout;
        private DateTime _lastFetch;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public CachedItem(Fetch getter, TimeSpan timeout) {
            _getter = getter ?? throw new ArgumentNullException(nameof(getter));
            _timeout = timeout;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public bool Retrieve(out T item, out string message, out HttpStatusCode code) {
            message = null;
            code = HttpStatusCode.ServiceUnavailable;

            if (DateTime.Now - _lastFetch > _timeout) {
                if (_getter(out item, out message, out code)) {
                    _available = true;
                    _cache = item;
                    _lastFetch = DateTime.Now;
                }
            }

            item = _cache;
            return _available;
        }

        public bool Reset() {
            _lastFetch = DateTime.MinValue;
            return true;
        }
        #endregion

    }
}
