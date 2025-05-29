using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OneDriveFileBackuper.Options;
using OneDriveFileBackuper.Options.Validators;
using OneDrivePhotoDownloader.Options;

namespace OneDriveFileBackuper.Configurations
{
    /// <summary>
    /// Extension class for option configuration
    /// </summary>
    public static class OptionConfiguration
    {
        /// <summary>
        /// Configure option
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="builder">Builder</param>
        public static void ConfigureOptions(this IServiceCollection services, IConfigurationRoot builder)
        {
            services.AddOptions<EntraIdOption>()
                .Bind(builder.GetSection(EntraIdOption.ConfigurationSectionName))
                .ValidateOnStart();

            services.AddOptions<OneDriveOption>()
                .Bind(builder.GetSection(OneDriveOption.ConfigurationSectionName))
                .ValidateOnStart();

            services.AddOptions<BackupSettingsOption>()
                .Bind(builder.GetSection(BackupSettingsOption.ConfigurationSectionName))
                .ValidateOnStart();

            services.AddOptions<LocalStorageOption>()
                .Bind(builder.GetSection(LocalStorageOption.ConfigurationSectionName))
                .ValidateOnStart();

            services.AddScoped<IValidateOptions<EntraIdOption>, EntraIdOptionValidator>();
            services.AddScoped<IValidateOptions<OneDriveOption>, OneDriveOptionValidator>();
            services.AddScoped<IValidateOptions<LocalStorageOption>, LocalStorageOptionValidator>();
        }

        /// <summary>
        /// Validate all options
        /// </summary>
        /// <param name="provider">Service provider</param>
        public static void ValidateOptions(this IServiceProvider provider)
        {
            _ = provider.GetRequiredService<IOptions<EntraIdOption>>().Value;
            _ = provider.GetRequiredService<IOptions<OneDriveOption>>().Value;
            _ = provider.GetRequiredService<IOptions<BackupSettingsOption>>().Value;
        }
    }
}
