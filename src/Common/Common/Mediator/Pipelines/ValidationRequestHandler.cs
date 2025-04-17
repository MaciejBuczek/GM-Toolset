using FluentValidation;

namespace Common.Mediator.Pipelines
{
    public sealed class ValidationRequestHandler<TRequest, TResponse>(
            IRequestHandler<TRequest, TResponse> innerHandler,
            IEnumerable<IValidator<TRequest>> validators) : IRequestHandler<TRequest, TResponse>
            where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults =
                await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures =
                validationResults
                .Where(r => r.Errors.Count != 0)
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await innerHandler.Handle(request, cancellationToken);
        }
    }
}
