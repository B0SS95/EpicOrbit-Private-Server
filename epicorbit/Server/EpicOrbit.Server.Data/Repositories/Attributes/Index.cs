using EpicOrbit.Server.Data.Repositories.Attributes.Abstracts;
using MongoDB.Driver;
using System;

namespace EpicOrbit.Server.Data.Repositories.Attributes {
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false)]
    public class Index : MongoAttributeBase {

        private readonly TimeSpan? expireAfter;
        private readonly bool isUnique;

        public Index(bool isUnique = true, int expireAfter = 0) {
            this.isUnique = isUnique;
            if (expireAfter > 0) {
                this.expireAfter = TimeSpan.FromSeconds(expireAfter);
            }
        }

        public static void ProcessAttributes<T>(IMongoCollection<T> collection) {
            GetAttributes<Index, T>().ForEach(x =>
               collection.Indexes.CreateOne(Builders<T>.IndexKeys.Ascending(x.Item1), new CreateIndexOptions<T>() {
                   Unique = x.Item2.isUnique,
                   ExpireAfter = x.Item2.expireAfter
               })
            );
        }
    }
}
