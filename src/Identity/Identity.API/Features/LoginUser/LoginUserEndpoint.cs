
namespace Identity.API.Features.LoginUser
{
    public record LoginUserResponse(bool Success);
    public record LoginUserRequest(string Email, string Password);

    public class LoginUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/login",
                async (LoginUserRequest request,
                IRequestHandler<LoginUserQuery,
                LoginUserResult> handler,
                CancellationToken cancellationToken) =>
                {
                    var command = request.Adapt<LoginUserQuery>();
                    var result = await handler.Handle(command, cancellationToken);
                    var response = result.Adapt<LoginUserResponse>();

                    return response.Success ? Results.Ok() : Results.BadRequest();
                })
                .WithName("LoginUser")
                .WithTags("Identity")
                .Produces<LoginUserResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithOpenApi()
                ;
                //.WithOpenApi(operation =>
                //{
                //    operation.RequestBody = DefaultRequestProvider.LoginUserRequest();
                //    return operation;
                //});
        }
    }
}
