using EpicOrbit.Server.Data.Repositories.Attributes.Abstracts;
using EpicOrbit.Server.Data.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EpicOrbit.Server.Data.Repositories {
    internal class Collection {

        internal static readonly List<MethodInfo> Attributes;
        static Collection() {
            Attributes = typeof(Collection).Assembly.GetTypes()
                .Where(x => x.BaseType == typeof(MongoAttributeBase))
                .Select(x => x.GetMethod("ProcessAttributes"))
                .ToList();
        }

    }

    internal class Collection<T> {

        private readonly IDatabaseContext context;
        private readonly string name;

        public Collection(IDatabaseContext context, string name = null) {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.name = name ?? typeof(T).Name;
        }

        private object _lock = new object();
        private IMongoCollection<T> _collection = null;

        public IMongoCollection<T> GetCollection() {
            lock (_lock) {
                if (_collection == null) {
                    int count = context.GetConnection().ListCollections().ToListAsync()
                        .GetAwaiter().GetResult().Where(x => x.GetElement("name").Value == name.ToLowerInvariant()).Count();
                    if (count <= 0) { // create collection
                        context.GetConnection().CreateCollection(name.ToLowerInvariant());
                    }

                    _collection = context.GetConnection().GetCollection<T>(name.ToLowerInvariant());

                    if (count <= 0) {
                        Collection.Attributes.Select(x => x.MakeGenericMethod(typeof(T)))
                            .ToList().ForEach(x => x.Invoke(null, new object[] { _collection }));
                    }
                }
                return _collection;
            }
        }


    }

}
