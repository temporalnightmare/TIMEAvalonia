using Microsoft.Extensions.DependencyInjection;
using TIME.Library.Interfaces;
using TIME.Library.Services;
using TIME.UI.ViewModels;
using TIME.UI.Views;

namespace TIME.UI;

internal sealed class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        using (var serviceProvider = serviceCollection.BuildServiceProvider()) 
        {
            // Start the WPF Application
            var app = new App();
            app.InitializeComponent();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            app.Run();
        }      
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register Views
        services.AddSingleton<MainWindow>();
        services.AddTransient<PageDashboardView>(); 
        services.AddTransient<PageBakeryView>();
        services.AddTransient<PageHashGeneratorView>();
        services.AddTransient<PageHideFileEditorView>();
        services.AddTransient<PageStringEditorView>();

        // Register ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<PageDashboardViewModel>();
        services.AddTransient<PageBakeryViewModel>();
        services.AddTransient<PageHashGeneratorViewModel>();
        services.AddTransient<PageHideFileEditorViewModel>();
        services.AddTransient<PageStringEditorViewModel>();

        // Register Services
        services.AddSingleton<IFileDialog, FileDialog>();
    }
}

