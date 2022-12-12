namespace Checkout.PaymentGateway.Api.Service.AcquiringBank
{
    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException(string? message, Exception? innerException)
            :base(message, innerException) { }
    }
}