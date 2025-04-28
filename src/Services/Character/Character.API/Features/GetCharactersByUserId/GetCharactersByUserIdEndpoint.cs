namespace Character.API.Features.GetCharactersByUserId
{
    public record GetCharactersByUserIdResponse(IEnumerable<Entities.Character> Characters);

    public class GetCharactersByUserIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/characters/user/{id}",
                async (Guid id,
                IRequestHandler<GetCharactersByUserIdQuery,
                GetCharactersByUserIdResult> handler,
                CancellationToken cancellationToken) =>
                {
                    var command = new GetCharactersByUserIdQuery(id);
                    var result = await handler.Handle(command, cancellationToken);
                    var response = result.Adapt<GetCharactersByUserIdResponse>();

                    return Results.Ok(response);
                })
                .WithName("GetCharacterByUserId")
                .WithTags("Character")
                .Produces<CreateCharacterResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithOpenApi();
        }
    }
}
