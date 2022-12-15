namespace Checkout.PaymentGateway.Api.Service.AcquiringBank
{
    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException(Exception? innerException)
            :base("Payment processing error", innerException) { }
    }
}