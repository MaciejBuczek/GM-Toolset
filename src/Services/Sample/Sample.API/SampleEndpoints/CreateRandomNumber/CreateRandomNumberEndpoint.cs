
using Carter;
using Common.Mediator;

namespace Sample.API.SampleEndpoints.CreateRandomNumber
{
    public class CreateRandomNumberEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("numbers/", async (
                CreateRandomNumberRequest request,
                IRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse> handler,
                CancellationToken cancellationToken) =>
                    {
                        var result = await handler.Handle(new CreateRandomNumberRequest(request.maxNumber), cancellationToken);

                        return result;
                    })
        .WithName("CreateRandomNumber")
        .WithTags("Numbers")
        .Produces<CreateRandomNumberResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi();
        }
    }
}
