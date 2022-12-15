using Checkout.PaymentGateway.AcquiringBankSimulator.Dto;
using Checkout.PaymentGateway.AcquiringBankSimulator.Service;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.AcquiringBankSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentProcessingService _paymentProcessingService;

        public PaymentsController(IPaymentProcessingService paymentProcessingService)
        {
            _paymentProcessingService = paymentProcessingService;
        }

        [HttpPost]
        [Route("/payments")]
        public async Task<IActionResult> MakePayment([FromBody] PaymentDto request)
        {
            var result = await _paymentProcessingService.ProcessPayment(request);

            return new OkObjectResult(result);
        }
    }
}