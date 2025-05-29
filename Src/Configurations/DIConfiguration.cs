using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OneDriveFileBackuper.Auth;
using OneDriveFileBackuper.GraphClient;
using OneDriveFileBackuper.Handlers;
using OneDriveFileBackuper.OneDrive;
using OneDriveFileBackuper.Storages;
using OneDrivePhotoDownloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriveFileBackuper.Configurations
{
    /// <summary>
    /// Extension class for configuration DI 
    /// </summary>
    public static class DIConfiguration
    {
        /// <summary>
        /// Configure services for DI
        /// </summary>
        /// <param name="services">Service collections</param>
        public static void ConfigureDI(this IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddConsole());
            services.AddSingleton<EntraIdAuthTokenProvider>();
            services.AddScoped<IAuthJwtTokenProvider, EntraIdAuthTokenProvider>();
            services.AddScoped<IJwtGraphServiceClient, JwtGraphServiceClient>();
            services.AddScoped<IOneDriveClient, OneDriveService>();
            services.AddScoped<IFileStorage, LocalFileStorage>();
            services.AddScoped<IFileSyncHandler, FileSyncHandler>();
            // Application register
            services.AddTransient<App>();
        }
    }
}
