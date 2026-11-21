using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ZooShop.Data;

namespace ZooShop
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("ZooShop application starting");

                var services = new ServiceCollection();

                services.AddSingleton<IConfiguration>(configuration);

                services.AddLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSerilog(Log.Logger, dispose: false);
                });

                DependencyInjection.ConfigureServices(services, configuration);

                using var serviceProvider = services.BuildServiceProvider();

                using var scope = serviceProvider.CreateScope();

                Application.Run(
                    scope.ServiceProvider.GetRequiredService<MainForm>());
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
                MessageBox.Show(
                    "Произошла критическая ошибка приложения.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                Log.Information("ZooShop application stopped");
                Log.CloseAndFlush();
            }
        }
    }
}
