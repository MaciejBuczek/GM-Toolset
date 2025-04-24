var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(config =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new ApplicationException("Connection string is empty");
    }
    config.Connection(connectionString);
}).UseLightweightSessions();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/appinfo", () =>
{
    var appInfo = new
    {
        ServiceName = Assembly.GetExecutingAssembly().GetName().Name,
        Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
    };

    return appInfo;
});

app.Run();