namespace Checkout.PaymentGateway.Api.Service
{
    public interface IValidationService<in T>
    {
        bool Validate(T entity);
    }
}