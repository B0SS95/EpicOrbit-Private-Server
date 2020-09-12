using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EpicOrbit.Server.Data.Extensions {
    public static class ActivatorExtension {

        public static object CreateInstance(this Type type) {
            if (type.GetConstructor(new Type[0]) != null) {
                return Activator.CreateInstance(type);
            }
            return Activator.CreateInstance(type, BindingFlags.CreateInstance
                | BindingFlags.Public | BindingFlags.Instance | BindingFlags.OptionalParamBinding,
                null, new Object[] { Type.Missing }, null);
        }

    }
}
