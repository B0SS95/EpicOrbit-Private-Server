using EpicOrbit.Server.Data.Repositories.Attributes.Abstracts;
using MongoDB.Driver;
using System;
using System.Linq;

namespace EpicOrbit.Server.Data.Repositories.Attributes {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class CompoundIndex : MongoAttributeBase {

        private readonly int id;
        private readonly bool isUnique;

        public CompoundIndex(int id, bool isUnique = true) {
            this.id = id;
            this.isUnique = isUnique;
        }

        public static void ProcessAttributes<T>(IMongoCollection<T> collection) {
            var grouping = GetAttributes<CompoundIndex, T>().GroupBy(x => x.Item2.id);

            if (grouping.Any(x => x.Select(y => y.Item2.isUnique).Distinct().Count() > 1)) {
                throw new InvalidOperationException("CompoundIndex with different unique values found!");
            }

            grouping.Select(x => new { ID = x.Key, Items = x.Select(y => y.Item1).ToArray(), IsUniqe = x.Select(y => y.Item2.isUnique).First() })
                .ToList().ForEach(x => {

                    var keys = Builders<T>.IndexKeys.Ascending(x.Items[0]);
                    for (int i = 1; i < x.Items.Length; i++) {
                        keys = keys.Ascending(x.Items[i]);
                    }

                    collection.Indexes.CreateOne(keys, new CreateIndexOptions<T>() {
                        Unique = x.IsUniqe
                    });
                });
        }

    }
}
