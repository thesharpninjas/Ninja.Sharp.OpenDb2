// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninja.Sharp.OpenDb2.Utilities;
using OpenDb2.Drivers.Linux;
using OpenDb2.Drivers.Windows;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Interfaces.Windows;
using System.Runtime.InteropServices;

namespace OpenDb2.Services
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers the Db2 services based on the current operating system platform.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static IServiceCollection AddDb2Services(
            this IServiceCollection services, 
            string connectionString, 
            IConfiguration config)
        {
            ConfigurationBuilder builder = new();

            builder.AddConfiguration(config);
            builder.Build();

            switch (new EnvironmentDetector().GetCurrentOSPlatform())
            {
                case var os when os == OSPlatform.Windows:
                    services.AddScoped<IWinDb2Connection>(sp => new WinDb2Connection(connectionString));
                    break;
                case var os when os == OSPlatform.Linux:
                    services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));
                    break;
                case var os when os == OSPlatform.OSX:
                    services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));
                    break;
                default:
                    throw new NotSupportedException("Unsupported operating system platform.");
            }

            return services;
        }

        /// <summary>
        /// Registers the Db2 services for Windows environment.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddWinDb2Services(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            ConfigurationBuilder builder = new();

            builder.AddConfiguration(config);
            builder.Build();

            services.AddScoped<IWinDb2Connection>(sp => new WinDb2Connection(connectionString));

            return services;
        }

        /// <summary>
        /// Registers the Db2 services for Linux environment.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <param name="config"></param>
        /// <returns></returns>
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
