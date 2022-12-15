namespace Checkout.PaymentGateway.Api.Queries
{
    public interface IQueryHandler<in TQuery, TResult>
    {
        Task<TResult?> Execute(TQuery query);
    }
}