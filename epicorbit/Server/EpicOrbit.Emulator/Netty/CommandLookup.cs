using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpicOrbit.Emulator.Netty {
    /// <summary>
    /// this class handles the lookup and creation of an ICommand
    /// </summary>
    public class CommandLookup : ICommandLookup {

        #region Static
        internal static ICommand CreateInstance(Type type, IGameLogger logger) {
            try {
                return type.CreateInstance() as ICommand;
            } catch (Exception e) {
                logger?.LogError(e);
            }
            return null;
        }
        #endregion

        private readonly IGameLogger _logger;
        private readonly Dictionary<short, Type> _lookup;

        public CommandLookup(IGameLogger logger) {
            _logger = logger;
            _lookup = new Dictionary<short, Type>();
        }

        /// <summary>
        /// Load all commands from TCurrentClass' assembly 
        /// </summary>
        /// <typeparam name="TCurrentClass">the class class</typeparam>
        /// <returns>the amount of commands loaded, if an error occurs the result is -1</returns>
        public int LoadCommands<TCurrentClass>(string identifier) {
            try {
                List<ICommand> commands = typeof(TCurrentClass).Assembly.GetTypes()
                    .Where(x => x.IsClass && !x.IsAbstract && typeof(ICommand).IsAssignableFrom(x)
                        && x.GetCustomAttributes<AutoDiscoverAttribute>().Count() == 1
                        && x.GetCustomAttribute<AutoDiscoverAttribute>().Identifier == identifier)
                    .Select(x => CreateInstance(x, _logger))
                    .Where(x => x != null && x.ID >= 0)
                    .ToList();

                commands.ForEach(x => {
                    if (_lookup.ContainsKey(x.ID)) {
                        _logger?.LogWarning($"Cannot add command '{x.GetType().Name}' with '{x.ID}' " +
                            $"as identifier, because there is already a command with this identifier!");
                        return;
                    }

                    try {
                        _lookup.Add(x.ID, x.GetType());
                    } catch (Exception e) {
                        _logger?.LogError(e);
                    }

                });
                return commands.Count;
            } catch (Exception e) {
                _logger?.LogError(e);
            }
            return -1;
        }

        /// <summary>
        /// Reads an ICommand from an IDataInput
        /// </summary>
        /// <param name="dataInput">the stream which contains data about an ICommand</param>
        /// <returns>ICommand</returns>
        public ICommand Lookup(IDataInput dataInput) {
            try {
                short id = dataInput.ReadShort();
                if (_lookup.TryGetValue(id, out Type type)) {
                    ICommand command = CreateInstance(type, _logger);
                    //    command.Read(dataInput, this);

                    return command;
                }
                _logger?.LogWarning($"No command with identifier '{id}' found!");
            } catch (Exception e) {
                _logger?.LogError(e);
            }
            return null;
        }

    }
}
