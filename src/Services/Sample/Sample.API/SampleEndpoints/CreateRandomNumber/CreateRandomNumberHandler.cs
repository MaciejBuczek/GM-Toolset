using Common.Mediator;
using FluentValidation;

namespace Sample.API.SampleEndpoints.CreateRandomNumber
{
    public record CreateRandomNumberRequest(int maxNumber);
    public record CreateRandomNumberResponse(int randomNumber);

    public class CreateRandomNumberCommandValidator : AbstractValidator<CreateRandomNumberRequest>
    {
        public CreateRandomNumberCommandValidator()
        {
            RuleFor(x => x.maxNumber).GreaterThan(0).WithMessage("Number must be greater than 0");
        }
    }
    internal class CreateRandomNumberCommandHandler
        : IRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>
    {
        public Task<CreateRandomNumberResponse> Handle(CreateRandomNumberRequest request, CancellationToken cancellationToken)
        {
            var random = new Random();
            var response = new CreateRandomNumberResponse(random.Next(request.maxNumber));

            return Task.FromResult(response);
        }
    }
}
