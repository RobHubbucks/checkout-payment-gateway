using Checkout.PaymentGateway.Api.Model;

namespace Checkout.PaymentGateway.Api.Dto.Mapping
{
    public class PaymentResultMapper : IMapper<PaymentResultDto, PaymentResult>
    {
        public PaymentResult? Map(PaymentResultDto? input)
        {
            throw new NotImplementedException();
        }

        public PaymentResultDto? Map(PaymentResult? input)
        {
            if (input == null)
                return null;

            return new PaymentResultDto(input.MerchantReference, input.PaymentId, input.Status);
        }
    }
}