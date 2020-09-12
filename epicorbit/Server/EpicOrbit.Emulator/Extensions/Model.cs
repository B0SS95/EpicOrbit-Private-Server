using EpicOrbit.Emulator;
using MongoDB.Driver;
using System.Collections.Generic;

namespace FightZone.Emulator.Extensions {
    public static class Model<T> where T : class {

        public static IAsyncEnumerable<T> AsQueryable(string name = null) {
            return GameContext.Database.AsQueryable<T>(name);
        }

        public static IMongoCollection<T> AsCollection(string name = null) {
            return GameContext.Database.GetCollection<T>(name);
        }

    }
}
