using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace EpicOrbit.Shared.Implementations {
    public class LoggerProvider : ILoggerProvider {

        #region {[ FIELDS ]}
        private readonly ConcurrentDictionary<string, LoggerWrapper> _loggers = new ConcurrentDictionary<string, LoggerWrapper>();
        private readonly IGameLogger _logger;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public LoggerProvider(IGameLogger logger) {
            _logger = logger;
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public ILogger CreateLogger(string categoryName) {
            return _loggers.GetOrAdd(categoryName, name => new LoggerWrapper(name, _logger));
        }

        public void Dispose() {
            _loggers.Clear();
        }
        #endregion

    }
}
