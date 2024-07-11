using System.Collections.Concurrent;

namespace Coding.Challenge.Dependencies.Database
{

/**
*
*   NAO ALTERAR ESTE ARQUIVO
*
**/
    public class DatabaseLenta<TOut, TIn> : IDatabase<TOut, TIn>
    {
        private readonly ConcurrentDictionary<Guid, TOut?> _database;
        private readonly IMapper<TOut, TIn> _mapper;
        public DatabaseLenta(IMapper<TOut?, TIn> mapper, IDadosMockados<TOut> mockData)
        {
            _mapper = mapper;
            _database = new ConcurrentDictionary<Guid, TOut?>();

            var mocks = mockData.GerarMocks();
            foreach (var mock in mocks)
            {
                _database.TryAdd(mock.Key, mock.Value);
            }
        }

        public async Task<TOut?> Create(TIn item)
        {   
            var id = Guid.NewGuid();
            Thread.Sleep(1000);
            var createdItem = _mapper.Map(id, item);
            if(!_database.TryAdd(id, createdItem))
            {
                throw new Exception("Nao foi possivel adicionar conteudo ao banco de dados");
            }
            return await Task.FromResult(createdItem);
        }

        public Task<Guid> Delete(Guid id)
        {
            Thread.Sleep(2000);
            return Task.FromResult(_database.TryRemove(id, out _) ? id : Guid.Empty);
        }

        public Task<TOut?> Read(Guid id)
        {
            Thread.Sleep(3000);
            return Task.FromResult(_database.TryGetValue(id, out var item) ? item: default);    
        }

        public Task<IEnumerable<TOut?>> ReadAll()
        {
            Thread.Sleep(5000);
            return Task.FromResult(_database.Values.AsEnumerable());
        }

        public Task<TOut?> Update(Guid id, TIn item)
        {
            Thread.Sleep(1000);
            if (!_database.TryGetValue(id, out var dbItem))
            {
                return Task.FromResult<TOut>(default!)!;
            }
            var updatedItem = _mapper.Patch(dbItem!, item);
            if (!_database.TryUpdate(id, updatedItem, dbItem))
            {
                return Task.FromResult<TOut>(dbItem!)!;
            }
            return Task.FromResult(updatedItem);
        }
    }
}
