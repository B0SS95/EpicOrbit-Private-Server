using EpicOrbit.Server.Data.Repositories.Attributes.Abstracts;
using MongoDB.Driver;
using System;

namespace EpicOrbit.Server.Data.Repositories.Attributes {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class PartialIndex : MongoAttributeBase {

        private readonly bool isUnique;

        public PartialIndex(bool isUnique = true) {
            this.isUnique = isUnique;
        }

        public static void ProcessAttributes<T>(IMongoCollection<T> collection) {
            GetAttributes<PartialIndex, T>().ForEach(x => collection.Indexes.CreateOne(Builders<T>.IndexKeys.Ascending(x.Item1), new CreateIndexOptions<T>() {
                Unique = x.Item2.isUnique,
                PartialFilterExpression = Builders<T>.Filter.Exists(x.Item1)
            }));
        }

    }
}
