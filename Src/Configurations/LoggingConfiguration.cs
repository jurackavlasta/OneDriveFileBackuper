using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriveFileBackuper.Configurations
{
    /// <summary>
    /// Configuration for logging
    /// </summary>
    public static class LoggingConfiguration
    {
        /// <summary>
        /// Configure logging by section 'Logging' in appsettings.json
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="builder">Builder</param>
        public static void ConfigureLogging(this IServiceCollection services, IConfigurationRoot builder)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConfiguration(builder.GetSection("Logging"));
                loggingBuilder.AddConsole();
            });
        }
    }
}
