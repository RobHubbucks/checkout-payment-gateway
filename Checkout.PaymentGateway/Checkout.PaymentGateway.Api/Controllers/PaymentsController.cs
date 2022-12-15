using System.Net;
using Checkout.PaymentGateway.Api.Commands;
using Checkout.PaymentGateway.Api.Dto;
using Checkout.PaymentGateway.Api.Dto.Mapping;
using Checkout.PaymentGateway.Api.Model;
using Checkout.PaymentGateway.Api.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper<PaymentRequestDto, PaymentRequestCommand> _paymentRequestMapper;
        private readonly IMapper<PaymentDetailsDto, PaymentDetails> _paymentDetailsDtoMapper;
        private readonly IMapper<PaymentResultDto, PaymentResult> _paymentResultMapper;
        private readonly ICommandHandler<PaymentRequestCommand, PaymentResult> _paymentRequestCommandHandler;
        private readonly IQueryHandler<GetPaymentDetailsQuery, PaymentDetails> _getPaymentDetailsQueryHandler;

        public PaymentsController(
            IMapper<PaymentRequestDto, PaymentRequestCommand> paymentRequestMapper,
            IMapper<PaymentDetailsDto, PaymentDetails> paymentDetailsDtoMapper, 
            IMapper<PaymentResultDto, PaymentResult> paymentResultMapper, 
            ICommandHandler<PaymentRequestCommand, PaymentResult> paymentRequestCommandHandler,
            IQueryHandler<GetPaymentDetailsQuery, PaymentDetails> getPaymentDetailsQueryHandler)
        {
            _paymentRequestMapper = paymentRequestMapper;
            _paymentDetailsDtoMapper = paymentDetailsDtoMapper;
            _paymentResultMapper = paymentResultMapper;
            _paymentRequestCommandHandler = paymentRequestCommandHandler;
            _getPaymentDetailsQueryHandler = getPaymentDetailsQueryHandler;
        }

        /// <summary>
        /// Make a payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/payments")]
        [ProducesResponseType(typeof(PaymentResultDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> MakePayment([FromBody] PaymentRequestDto request)
        {
            var paymentRequest = _paymentRequestMapper.Map(request);

            if (paymentRequest == null)
                return BadRequest();

            var result = await _paymentRequestCommandHandler.Handle(paymentRequest);

            var resultDto = _paymentResultMapper.Map(result);

            return new OkObjectResult(resultDto);
        }

        /// <summary>
        /// Retrieve details for a previous payment
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/payments/{paymentId}")]
        [ProducesResponseType(typeof(PaymentDetailsDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPayment(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
                return BadRequest();

            var payment = await _getPaymentDetailsQueryHandler.Execute(new GetPaymentDetailsQuery(paymentId));

            if (payment == null)
                return NotFound();

            var paymentDetailsDto = _paymentDetailsDtoMapper.Map(payment);

            return new OkObjectResult(paymentDetailsDto);
        }
    }
}