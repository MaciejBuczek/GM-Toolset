using Common.Mediator;

namespace Sample.API.SampleEndpoints.CreateRandomNumber
{
    public record CreateRandomNumberRequest(int maxNumber);
    public record CreateRandomNumberResponse(int randomNumber);
    public class CreateRandomNumberCommandHandler
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
