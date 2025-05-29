using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneDriveFileBackuper.Auth;
using OneDriveFileBackuper.Configurations;
using OneDriveFileBackuper.GraphClient;
using OneDriveFileBackuper.Handlers;
using OneDriveFileBackuper.OneDrive;
using OneDriveFileBackuper.Options;
using OneDriveFileBackuper.Options.Validators;
using OneDriveFileBackuper.Storages;
using OneDrivePhotoDownloader;
using OneDrivePhotoDownloader.Options;

var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true)
                 .AddUserSecrets<Program>()
                 .Build();

// Registration services
var services = new ServiceCollection();

// Mapping of options
services.ConfigureOptions(builder);

// Register services
services.ConfigureDI();

// Provider
var provider = services.BuildServiceProvider();

// Option validate
provider.ValidateOptions();

// Run app
var app = provider.GetRequiredService<App>();

await app.RunAsync();
