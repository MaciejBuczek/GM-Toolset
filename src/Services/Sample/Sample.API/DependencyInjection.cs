using Common.Mediator;
using Sample.API.SampleEndpoints.CreateRandomNumber;

namespace Sample.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<CreateRandomNumberCommandHandler>();

            services.AddScoped<IRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>>(sp =>
                new LoggingRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>(
                    sp.GetRequiredService<CreateRandomNumberCommandHandler>(),
                    sp.GetRequiredService<ILogger<LoggingRequestHandler<CreateRandomNumberRequest, CreateRandomNumberResponse>>>()));

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            return app;
        }
    }
}
