// (c) 2024 thesharpninjas
// This code is licensed under MIT license (see LICENSE.txt for details)

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ninja.Sharp.OpenDb2.Interfaces;
using Ninja.Sharp.OpenDb2.Utilities;
using OpenDb2.Interfaces;
using OpenDb2.Drivers.Linux;
using OpenDb2.Drivers.Windows;
using OpenDb2.Interfaces.Linux;
using OpenDb2.Interfaces.Windows;
using System.Runtime.InteropServices;

namespace OpenDb2.Services
{
    /// <summary>
    /// Provides extension methods on <see cref="IServiceCollection"/> for registering DB2 connection services.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Registers the appropriate DB2 connection as a scoped service based on the current operating system.
        /// On Windows it registers <see cref="IWinDb2Connection"/>; on Linux and macOS it registers <see cref="ILnxDb2Connection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="connectionString">The DB2 connection string.</param>
        /// <param name="config">The application configuration (reserved for future use).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <exception cref="NotSupportedException">Thrown when the current OS platform is not supported.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is null, empty, or whitespace.</exception>
        public static IServiceCollection AddDb2Services(
            this IServiceCollection services, 
            string connectionString, 
            IConfiguration config)
        {
            return AddDb2Services(services, connectionString, config, new EnvironmentDetector());
        }

        /// <summary>
        /// Registers the appropriate DB2 connection as a scoped service based on the detected operating system.
        /// This overload accepts an <see cref="IEnvironmentDetector"/> for testability.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="connectionString">The DB2 connection string.</param>
        /// <param name="config">The application configuration (reserved for future use).</param>
        /// <param name="environmentDetector">The detector used to identify the current OS platform.</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <exception cref="NotSupportedException">Thrown when the current OS platform is not supported.</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> or <paramref name="environmentDetector"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is null, empty, or whitespace.</exception>
        public static IServiceCollection AddDb2Services(
            this IServiceCollection services,
            string connectionString,
            IConfiguration config,
            IEnvironmentDetector environmentDetector)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
            ArgumentNullException.ThrowIfNull(environmentDetector);

            switch (environmentDetector.GetCurrentOSPlatform())
            {
                case var os when os == OSPlatform.Windows:
                    services.AddScoped<IWinDb2Connection>(sp => new WinDb2Connection(connectionString));
                    services.AddScoped<IDb2Connection>(sp => sp.GetRequiredService<IWinDb2Connection>());
                    break;
                case var os when os == OSPlatform.Linux:
                    services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));
                    services.AddScoped<IDb2Connection>(sp => sp.GetRequiredService<ILnxDb2Connection>());
                    break;
                case var os when os == OSPlatform.OSX:
                    services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));
                    services.AddScoped<IDb2Connection>(sp => sp.GetRequiredService<ILnxDb2Connection>());
                    break;
                default:
                    throw new NotSupportedException("Unsupported operating system platform.");
            }

            return services;
        }

        /// <summary>
        /// Registers <see cref="IWinDb2Connection"/> as a scoped service for Windows environments.
        /// Use this method when the application is known to run exclusively on Windows.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="connectionString">The OleDb connection string for the DB2 instance.</param>
        /// <param name="config">The application configuration (reserved for future use).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is null, empty, or whitespace.</exception>
        public static IServiceCollection AddWinDb2Services(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            services.AddScoped<IWinDb2Connection>(sp => new WinDb2Connection(connectionString));
            services.AddScoped<IDb2Connection>(sp => sp.GetRequiredService<IWinDb2Connection>());

            return services;
        }

        /// <summary>
        /// Registers <see cref="ILnxDb2Connection"/> as a scoped service for Linux and macOS environments.
        /// Use this method when the application is known to run exclusively on Linux or macOS.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="connectionString">The IBM DB2 connection string for the DB2 instance.</param>
        /// <param name="config">The application configuration (reserved for future use).</param>
        /// <returns>The same <see cref="IServiceCollection"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="connectionString"/> is null, empty, or whitespace.</exception>
        public static IServiceCollection AddLnxDb2Services(this IServiceCollection services, string connectionString, IConfiguration config)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

            services.AddScoped<ILnxDb2Connection>(sp => new LnxDb2Connection(connectionString));
            services.AddScoped<IDb2Connection>(sp => sp.GetRequiredService<ILnxDb2Connection>());

            return services;
        }
    }
}
