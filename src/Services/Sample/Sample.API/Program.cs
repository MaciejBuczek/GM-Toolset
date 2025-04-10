using Common.Mediator;
using Sample.API.SampleEndpoints.CreateRandomNumber;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CreateRandomNumberCommandHandler>();
builder.Services.AddScoped<IRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>>(sp =>
    new LoggingRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>(
        sp.GetRequiredService<CreateRandomNumberCommandHandler>(),
        sp.GetRequiredService<ILogger<LoggingRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>>>()));

// Add services to the container.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapGet("/HelloAzure", () =>
{
    return "Hello I live in the Azure Container App now!";
});

app.MapGet("/Common", () =>
{
    return Common.Sample.Test;
});

app.MapPost("numbers/", async (
    int maxNumber,
    IRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse> handler,
    CancellationToken cancellationToken) =>
{
    var number = await handler.Handle(new CreateRandomNumberRequest(maxNumber), cancellationToken);

    return number is not null ? Results.Ok(number) : Results.Problem();
})
.WithName("CreateRandomNumber")
.WithTags("Numbers")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
