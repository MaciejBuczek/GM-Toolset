using Microsoft.Extensions.Logging;

namespace Common.Mediator
{
    public interface IRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }

    public sealed class LoggingRequestHandler<TRequest, TResponse>(
        IRequestHandler<TRequest, TResponse> innerHandler,
        ILogger<LoggingRequestHandler<TRequest, TResponse>> logger) : IRequestHandler<TRequest, TResponse>
        where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Begin pipeline behavior {Request}", request.GetType().Name);

            var response = await innerHandler.Handle(request, cancellationToken);

            logger.LogInformation("End pipeline behavior {Request}", request.GetType().Name);

            return response;
        }
    }
}
