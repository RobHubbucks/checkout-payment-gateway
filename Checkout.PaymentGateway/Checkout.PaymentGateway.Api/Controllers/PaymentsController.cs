using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper<PaymentRequestDto, PaymentRequest> _paymentRequestMapper;
        private readonly IMapper<PaymentDetailsDto, PaymentDetails> _paymentDetailsDtoMapper;
        private readonly IMapper<PaymentResultDto, PaymentResult> _paymentResultMapper;
        private readonly IPaymentService _paymentService;

        public PaymentsController(
            IMapper<PaymentRequestDto, PaymentRequest> paymentRequestMapper,
            IMapper<PaymentDetailsDto, PaymentDetails> paymentDetailsDtoMapper, 
            IMapper<PaymentResultDto, PaymentResult> paymentResultMapper, 
            IPaymentService paymentService)
        {
            _paymentRequestMapper = paymentRequestMapper;
            _paymentDetailsDtoMapper = paymentDetailsDtoMapper;
            _paymentResultMapper = paymentResultMapper;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto request)
        {
            var paymentRequest = _paymentRequestMapper.Map(request);

            if (paymentRequest == null)
                return BadRequest();

            var result = await _paymentService.RequestPayment(paymentRequest);

            var resultDto = _paymentResultMapper.Map(result);

            return new OkObjectResult(resultDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetPayment(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
                return BadRequest();

            var payment = await _paymentService.GetPayment(paymentId);

            if (payment == null)
                return NotFound();

            var paymentDetailsDto = _paymentDetailsDtoMapper.Map(payment);

            return new OkObjectResult(paymentDetailsDto);
        }
    }
}