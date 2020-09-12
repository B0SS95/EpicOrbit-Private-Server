using EpicOrbit.Server.Data.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpicOrbit.Server.Data.Repositories {
    public class DatabaseContext : IDatabaseContext {

        private static class CollectionWrapper<T> {

            private static Collection<T> instance;
            public static Collection<T> GetInstance(DatabaseContext context, string name = null) {
                if (instance == null) {
                    instance = new Collection<T>(context, name);
                }
                return instance;
            }

            public static void Refresh() {
                instance = null;
            }

        }

        private string _connectionString;
        private string _database;

        public DatabaseContext(string connectionString, string database) {
            _connectionString = connectionString;
            _database = database;
        }

        private object _lock = new object();
        private IMongoDatabase _instance;
        private bool _refresh = true;

        public IMongoDatabase GetConnection() {
            lock (_lock) {
                if (_instance == null || _refresh) {
                    _instance = new MongoClient(_connectionString).GetDatabase(_database);
                    _refresh = false;
                }
                return _instance;
            }
        }

        public Task<IClientSessionHandle> Session() {
            return _instance.Client.StartSessionAsync();
        }

        private HashSet<Type> _types = new HashSet<Type>();
        public IMongoCollection<T> GetCollection<T>(string name = null) {
            lock (_types) {
                _types.Add(typeof(T));
                return CollectionWrapper<T>.GetInstance(this, name).GetCollection();
            }
        }

        public IAsyncEnumerable<T> AsQueryable<T>(string name = null) {
            return GetCollection<T>(name).AsQueryable().ToAsyncEnumerable();
        }

        public void Refresh() {
            _refresh = true;
            lock (_types) {
                foreach (Type t in _types) {
                    typeof(CollectionWrapper<>).MakeGenericType(t).GetMethod("Refresh").Invoke(null, null);
                }
            }
        }
    }
}
