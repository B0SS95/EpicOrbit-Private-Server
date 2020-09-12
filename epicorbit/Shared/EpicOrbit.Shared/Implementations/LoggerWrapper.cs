using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicOrbit.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace EpicOrbit.Shared.Implementations {
    public class LoggerWrapper : ILogger {

        #region {[ FIELDS ]}
        private readonly IGameLogger _logger;
        private readonly string _name;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public LoggerWrapper(string name, IGameLogger logger) {
            _name = name;
            _logger = logger;
        }
        #endregion

        #region {[ INTERFACE - ILogger ]}
        public IDisposable BeginScope<TState>(TState state) {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return _logger.LogLevel <= logLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            if (!IsEnabled(logLevel)) {
                return;
            }

            switch (logLevel) {
                case LogLevel.Critical:
                    _logger.LogCritical(formatter(state, exception), _name, "wrp");
                    break;
                case LogLevel.Debug:
                    _logger.LogDebug(formatter(state, exception), _name, "wrp");
                    break;
                case LogLevel.Error:
                    _logger.LogError(new Exception(formatter(state, exception)), _name, "wrp");
                    break;
                case LogLevel.Information:
                    _logger.LogInformation(formatter(state, exception), _name, "wrp");
                    break;
                case LogLevel.Warning:
                    _logger.LogInformation(formatter(state, exception), _name, "wrp");
                    break;
            }
        }
        #endregion

    }
}
