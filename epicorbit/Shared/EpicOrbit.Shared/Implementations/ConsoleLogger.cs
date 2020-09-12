using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using EpicOrbit.Shared.Interfaces;
using Microsoft.Extensions.Logging;

namespace EpicOrbit.Shared.Implementations {
    public class ConsoleLogger : IGameLogger {

        #region {[ PROPERTIES ]}
        public LogLevel LogLevel => _loglevel;
        #endregion

        #region {[ FIELDS ]}
        private readonly object _lock;
        private readonly LogLevel _loglevel;
        #endregion

        #region {[ CONSTRUCTOR ]}
        public ConsoleLogger(LogLevel loglevel = LogLevel.Critical) {
            _lock = new object();
            _loglevel = loglevel;
        }
        #endregion

        #region {[ LOGGER - LOGIC ]}
        private void Log(string message, string membername, string filename, int line, [CallerMemberName] string logCaller = "") {

            if (filename == "wrp" && membername.Contains(".")) {
                string[] parts = membername.Split('.');
                if (parts.Length > 2) {
                    membername = string.Join(".", parts.Skip(parts.Length - 2).Take(2));
                }
            }

            string type = logCaller.Replace("Log", string.Empty);

            if (!LogLevelPass(type)) {
                return;
            }

            ConsoleColor color = GetColor(type);

            lock (_lock) {
                Console.ForegroundColor = color;

                if (filename == "wrp") {
                    Console.Write($"[{DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}] [{type}] [wrp:{membername}() Line: {line}] ");
                } else {
                    Console.Write($"[{DateTime.Now.ToString("dd.MM.yyyy hh:mm:ss")}] [{type}] [{Path.GetFileName(filename)}:{membername}() Line: {line}] ");
                }

                Console.ResetColor();
                Console.WriteLine(message);
            }
        }

        private bool LogLevelPass(string type) {
            if (_loglevel == LogLevel.None) {
                return false;
            }

            switch (type) {
                case "Debug":
                    return _loglevel <= LogLevel.Debug;
                case "Information":
                    return _loglevel <= LogLevel.Information;
                case "Success":
                    return _loglevel <= LogLevel.Warning;
                case "Warning":
                    return _loglevel <= LogLevel.Warning;
                case "Error":
                    return _loglevel <= LogLevel.Error;
                case "Critical":
                    return _loglevel <= LogLevel.Critical;
            }

            return true;
        }

        private ConsoleColor GetColor(string type) {
            switch (type) {
                case "Error":
                    return ConsoleColor.Red;

                case "Critical":
                    return ConsoleColor.Magenta;

                case "Information":
                    return ConsoleColor.Blue;

                case "Success":
                    return ConsoleColor.Green;

                case "Warning":
                    return ConsoleColor.Yellow;

                case "Debug":
                default:
                    return ConsoleColor.White;
            }
        }
        #endregion

        #region {[ FUNCTIONS ]}
        public void LogCritical(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(message, memberName, filePath, line);
        }

        public void LogDebug(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(message, memberName, filePath, line);
        }

        public Exception LogError(Exception error, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(error.ToString(), memberName, filePath, line);
            return error;
        }

        public void LogInformation(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(message, memberName, filePath, line);
        }

        public void LogSuccess(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(message, memberName, filePath, line);
        }

        public void LogWarning(string message, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0) {
            Log(message, memberName, filePath, line);
        }
        #endregion

    }
}
