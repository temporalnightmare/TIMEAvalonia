using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reflection;
using System.Windows.Controls;
using TIME.UI.Views;

namespace TIME.UI.ViewModels;

public partial class MainWindowViewModel : ViewBaseModel
{
    [ObservableProperty] private string _appName = "Titan IME";
    [ObservableProperty] private string _navItemDashboard = "Dashboard";
    [ObservableProperty] private string _navItemHashGenerator = "Hash Generator";
    [ObservableProperty] private string _navItemHashScanner = "Hash Scanner";
    [ObservableProperty] private string _navItemStringEditor = "String Editor";
    [ObservableProperty] private string _navItemHideFileEditor = "Hide File Editor";
    [ObservableProperty] private string _navItemBakery= "Bakery";
    [ObservableProperty] private string _navItemSettings = "Settings";
    [ObservableProperty] private string _versionInfo = Assembly.GetExecutingAssembly().ImageRuntimeVersion;
    [ObservableProperty] private UserControl _currentPage;

    [RelayCommand]
    private void ShowToolDashboard()
    {
        CurrentPage = new PageDashboardView();
    }

    [RelayCommand]
    private void ShowToolBakery()
    {
        CurrentPage = new PageBakeryView();
    }

    [RelayCommand]
    private void ShowToolHideFileEditor()
    {
        CurrentPage = new PageHideFileEditorView();
    }

    [RelayCommand]
    private void ShowToolHashScanner()
    {
        CurrentPage = new PageHashScannerView();
    }

    [RelayCommand]
    private void ShowToolStringEditor()
    {
        CurrentPage = new PageStringEditorView();
    }

    [RelayCommand]
    private void ShowToolHashGenerator()
    {
        CurrentPage = new PageHashGeneratorView();
    }

    [RelayCommand]
    private void ShowSettings()
    {
        CurrentPage = new PageSettingsView();
    }
}
