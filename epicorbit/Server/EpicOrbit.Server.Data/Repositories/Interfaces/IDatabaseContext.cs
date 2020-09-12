using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EpicOrbit.Server.Data.Repositories.Interfaces {
    public interface IDatabaseContext {

        IMongoDatabase GetConnection();
        Task<IClientSessionHandle> Session();
        IMongoCollection<T> GetCollection<T>(string name = null);
        IAsyncEnumerable<T> AsQueryable<T>(string name = null);
        void Refresh();

    }
}
