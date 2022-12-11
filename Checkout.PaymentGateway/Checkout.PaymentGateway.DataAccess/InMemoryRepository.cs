using System.Collections.Concurrent;

namespace Checkout.PaymentGateway.DataAccess
{
    public class InMemoryRepository<TId, TItem> : IRepository<TId, TItem> where TId : notnull
    {
        private readonly ConcurrentDictionary<TId, TItem> _collection;

        public InMemoryRepository()
        {
            _collection = new ConcurrentDictionary<TId, TItem>();
        }

        public async Task<TItem?> GetById(TId id)
        {
            if (_collection.TryGetValue(id, out var item))
                return await Task.FromResult(item);

            return default;
        }

        public Task Add(TId id, TItem item)
        {
            _collection.TryAdd(id, item);
            return Task.CompletedTask;
        }

        public Task Update(TId id, TItem item)
        {
            _collection.AddOrUpdate(id, item, (_, _) => item);
            return Task.CompletedTask;
        }
    }
}