using EpicOrbit.Server.Data.Extensions;
using EpicOrbit.Shared.Interfaces;
using EpicOrbit.Emulator.Netty.Attributes;
using EpicOrbit.Emulator.Netty.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace EpicOrbit.Emulator.Netty {
    /// <summary>
    /// this class contains information about the handlers for each ICommand
    /// </summary>
    public class HandlerLookup : IHandlerLookup {

        #region Static
        private static bool IsAssignableToGenericType(Type givenType, Type genericType) {
            var interfaceTypes = givenType.GetInterfaces();

            if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType)) {
                return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType) {
                return true;
            }

            Type baseType = givenType.BaseType;
            return baseType != null && IsAssignableToGenericType(baseType, genericType);
        }

        #endregion

        private readonly IGameLogger _logger;
        private readonly Dictionary<short, Delegate> _lookup;

        public HandlerLookup(IGameLogger logger) {
            _logger = logger;
            _lookup = new Dictionary<short, Delegate>();
        }

        /// <summary>
        /// load all handlers in TCurrentClass' assembly
        /// </summary>
        /// <typeparam name="TCurrentClass"></typeparam>
        /// <returns>amount of handlers loaded, or -1 (if an error occurs)</returns>
        public int LoadHandlers<TCurrentClass>(string identifier) {
            try {
                List<Type> handlers = typeof(TCurrentClass).Assembly.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && IsAssignableToGenericType(x, typeof(ICommandHandler<>))
                    && x.GetCustomAttributes<AutoDiscoverAttribute>().Count() == 1
                    && x.GetCustomAttribute<AutoDiscoverAttribute>().Identifier == identifier)
                .ToList();

                handlers.ForEach(x => {
                    short id = CommandLookup.CreateInstance(x.GetInterfaces()[0].GetGenericArguments()[0], _logger).ID;
                    if (_lookup.ContainsKey(id)) {
                        _logger?.LogWarning($"Cannot add handler '{x.GetType().Name}' for '{id}' " +
                            $"as identifier, because there is already a handler for this identifier!");
                        return;
                    }

                    try {
                        _lookup.Add(id, Delegate.CreateDelegate(typeof(Action<,>)
                            .MakeGenericType(typeof(IClient), x.GetInterfaces()[0].GetGenericArguments()[0]),
                                x.CreateInstance(), x.GetMethod("Execute")));
                    } catch (Exception e) {
                        _logger?.LogError(e);
                    }
                });

                return handlers.Count;
            } catch (Exception e) {
                _logger?.LogError(e);
            }
            return -1;
        }

        /// <summary>
        /// looks up the handler an executed it
        /// </summary>
        /// <param name="command">the command to be handles</param>
        /// <param name="initiator">the peer which sent this command</param>
        /// <returns></returns>
        public void Handle(ICommand command, IClient initiator) {
            try {

                if (_lookup.TryGetValue(command.ID, out Delegate handle)) {
                    handle.DynamicInvoke(initiator, command);
                    return;
                }

                _logger?.LogInformation($"No handler for command with '{command.ID}' as identifier found!");
                if (command.ID != 17210 && _logger?.LogLevel <= LogLevel.Debug) {
                    _logger?.LogDebug(JsonConvert.SerializeObject(command, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }));
                }

            } catch (Exception e) {
                _logger?.LogError(e);
            }
        }

    }
}
