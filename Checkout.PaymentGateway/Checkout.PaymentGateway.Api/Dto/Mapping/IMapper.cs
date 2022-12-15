namespace Checkout.PaymentGateway.Api.Dto.Mapping
{
    public interface IMapper<T, TU>
    {
        TU? Map(T? input);

        T? Map(TU? input);
    }
}