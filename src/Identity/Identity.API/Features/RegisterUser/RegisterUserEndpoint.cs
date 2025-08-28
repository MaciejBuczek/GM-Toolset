
namespace Identity.API.Features.RegisterUser
{
    public record RegisterUserResponse(bool Success);
    public record RegisterUserRequest(string Username, string Email, string Password, string ConfirmPassword);

    public class RegisterUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/register",
                async (RegisterUserRequest request,
                IRequestHandler<RegisterUserCommand,
                RegisterUserResult> handler,
                CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<RegisterUserCommand>();
                    var result = await handler.Handle(command, cancellationToken);
                    var response = result.Adapt<RegisterUserResponse>();

                    return response.Success ? Results.Created() : Results.BadRequest();
                })
                .WithName("RegisterUser")
                .WithTags("Identity")
                .Produces<RegisterUserResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithOpenApi();
        }
    }
}
