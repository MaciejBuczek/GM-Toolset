var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(builder.Configuration.GetConnectionString("Database"));

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