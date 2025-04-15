using Common;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/common", () =>
{
    return Sample.Test;
});

app.MapGet("/appinfo", () =>
{
    var appInfo = new
    {
        ServiceName = Assembly.GetExecutingAssembly().GetName().Name,
        Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
        ConnectionString1 = builder.Configuration.GetConnectionString("Database")?.Length,
    };

    return appInfo;
});

app.Run();