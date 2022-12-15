namespace Checkout.PaymentGateway.Api.Commands
{
    public interface ICommandHandler<in TCommand, TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}