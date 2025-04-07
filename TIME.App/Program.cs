using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TIME.App.ViewModel;
using TIME.Library.Interfaces;
using TIME.Library.Services;

namespace TIME.App;

public class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        try
        {
            // Configure Dependency Injection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Use 'using' to ensure disposal of the service provider
            using (var serviceProvider = serviceCollection.BuildServiceProvider())
            {
                // Start the WPF application
                var app = new App();
                try
                {
                    var mainView = serviceProvider.GetRequiredService<MainWindow>();
                    mainView.Show();

                    app.Run();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while running the application: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Application failed to start: {ex.Message}", "Fatal Error",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private static void ConfigureServices(IServiceCollection services)
    {
        // Configure database
        var configuration = new ConfigurationBuilder()
          //  .AddJsonFile("AppSettings.json", optional: true, reloadOnChange: true)
            .Build();



        // Register Services
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IFileDialog, FileDialog>();
        services.AddSingleton<IValidationService, ValidationService>();

        // Register Views
        services.AddTransient<MainWindow>();

        // Register ViewModels
        services.AddTransient<MainWindowViewModel>();
    }
}
