var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMarten(builder.Configuration.GetConnectionString("Database")?? string.Empty);
builder.Services.AddHandlers();
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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

app.MapCarter();

app.Run();