using Common;

var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("/common", () =>
{
    return Sample.Test;
});

app.Run();