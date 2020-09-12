using System;

namespace EpicOrbit.Emulator.Game.Implementations {
    public class TickInterval {

        #region {[ PROPERTIES ]}
        public double Timeout { get; set; }
        #endregion

        #region {[ FIELDS ]}
        private Action _callback;
        private object _lock;
        private double _lastTickChangeTime;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public TickInterval(Action callback, double timeout, double currentTime = 0) {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            Timeout = timeout;

            if (timeout < 10) {
                throw new ArgumentException(nameof(timeout));
            }
            _lock = new object();

            if (currentTime >= 0) {
                _lastTickChangeTime = currentTime;
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void Tick(double timeSinceLastTick) {
            lock (_lock) {
                if (_lastTickChangeTime + timeSinceLastTick >= Timeout) {
                    _lastTickChangeTime = (_lastTickChangeTime + timeSinceLastTick) % Timeout; // time correction

                    _callback();
                } else {
                    _lastTickChangeTime += timeSinceLastTick;
                }
            }
        }

        public void Reset(double timeout = -1, double currentTime = 0) {
            lock (_lock) {
                if (timeout > 0) {
                    Timeout = timeout;
                }

                _lastTickChangeTime = currentTime;
            }
        }
        #endregion

    }
}
