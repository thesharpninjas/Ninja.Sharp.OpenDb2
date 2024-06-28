using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenDb2.Drivers.Linux;
using OpenDb2.Drivers.Windows;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Interfaces.Windows;

namespace OpenDb2.Services
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWinDb2Services(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            ConfigurationBuilder builder = new();

            builder.AddConfiguration(config);
            builder.Build();

            services.AddScoped<IWinDb2Connection>(sp => new WinDb2Connection(connectionString));

            return services;
        }

        public static IServiceCollection AddLnxDb2Services(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            ConfigurationBuilder builder = new();

            builder.AddConfiguration(config);
            builder.Build();

            services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));

            return services;
        }
    }
}
