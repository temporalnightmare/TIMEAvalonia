using Microsoft.Extensions.DependencyInjection;
using TIME.Library.Interfaces;
using TIME.Library.Services;
using TIME.UI.ViewModels;
using TIME.UI.ViewModels.SubViewModels;
using TIME.UI.Views;
using TIME.UI.Views.SubViews;

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
        services.AddTransient<SubPageWWE2K24View>();

        // Register ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddTransient<PageDashboardViewModel>();
        services.AddTransient<PageBakeryViewModel>();
        services.AddTransient<PageHashGeneratorViewModel>();
        services.AddTransient<PageHideFileEditorViewModel>();
        services.AddTransient<PageStringEditorViewModel>();
        services.AddTransient<SubPageWWE2K24ViewModel>();

        // Register Services
        services.AddSingleton<IFileDialog, FileDialog>();
        services.AddSingleton<IServiceProvider>(sp => sp);
    }
}

