using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OneDriveFileBackuper;
using OneDriveFileBackuper.Configurations;

var builder = new ConfigurationBuilder()
                 .SetBasePath(AppContext.BaseDirectory)
                 .AddUserSecrets<Program>()
                 .AddJsonFile($"appsettings.json", false, true)
                 .Build();

// Registration services
var services = new ServiceCollection();

// Configure logging
services.ConfigureLogging(builder);

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
