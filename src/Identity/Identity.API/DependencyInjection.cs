using Carter;

namespace Identity.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<Program>()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddValidatorsFromAssembly(
                typeof(Program).Assembly,
                includeInternalTypes: true);

            services.Decorate(
                typeof(IRequestHandler<,>),
                typeof(ValidationRequestHandler<,>));
            services.Decorate(
                typeof(IRequestHandler<,>),
                typeof(LoggingRequestHandler<,>));

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            return app;
        }

        public static IServiceCollection AddAuthRepository(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            return services;
        }
    }
}
