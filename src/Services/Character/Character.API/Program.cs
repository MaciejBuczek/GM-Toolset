using Common;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

var test = builder.Configuration.GetConnectionString("Database");

app.UseHttpsRedirection();

app.MapGet("/common", () =>
{
    return Sample.Test;
});

app.Run();