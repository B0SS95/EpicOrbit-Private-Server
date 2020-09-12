using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpicOrbit.Server.Data.Repositories.Attributes.Abstracts {
    public abstract class MongoAttributeBase : Attribute {

        private static List<Tuple<string, Attr>> GetFromFields<Attr, T>() where Attr : MongoAttributeBase {
            return typeof(T).GetFields()
                            .Where(x => x.IsPublic && x.GetCustomAttributes<Attr>().Count() > 0)
                            .SelectMany(x => x.GetCustomAttributes<Attr>().Select(y => new Tuple<string, Attr>(x.Name, y)))
                            .ToList();
        }

        private static List<Tuple<string, Attr>> GetFromProperties<Attr, T>() where Attr : MongoAttributeBase {
            return typeof(T).GetProperties()
                            .Where(x => x.GetMethod.IsPublic && x.GetCustomAttributes<Attr>().Count() > 0)
                            .SelectMany(x => x.GetCustomAttributes<Attr>().Select(y => new Tuple<string, Attr>(x.Name, y)))
                            .ToList();
        }

        internal static List<Tuple<string, Attr>> GetAttributes<Attr, T>() where Attr : MongoAttributeBase {
            List<Tuple<string, Attr>> attributes = new List<Tuple<string, Attr>>(GetFromFields<Attr, T>());
            attributes.AddRange(GetFromProperties<Attr, T>());
            return attributes.ToList();
        }

    }
}
