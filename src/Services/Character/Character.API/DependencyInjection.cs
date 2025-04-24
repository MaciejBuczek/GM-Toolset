namespace Character.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMartem(this IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException("Connection string is empty");
            }

            services.AddMarten(config =>
            {
                config.Connection(connectionString);
            }).UseLightweightSessions();

            return services;
        }
    }
}