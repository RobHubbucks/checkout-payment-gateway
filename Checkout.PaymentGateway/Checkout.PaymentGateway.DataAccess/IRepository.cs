namespace Checkout.PaymentGateway.DataAccess
{
    public interface IRepository<in TId, TItem>
    {
        Task<TItem?> GetById(TId id);

        Task Add(TId id, TItem item);

        Task Update(TId id, TItem item);
    }
}