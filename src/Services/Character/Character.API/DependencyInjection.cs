namespace Character.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMartenConnection(this IServiceCollection services, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("Connection string is empty");
            }

            services.AddMarten(config => config.Connection(connectionString)).UseLightweightSessions();

            return services;
        }

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

        public static IServiceCollection AddCharacterRepository(this IServiceCollection services)
        {
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            return services;
        }
    }
}