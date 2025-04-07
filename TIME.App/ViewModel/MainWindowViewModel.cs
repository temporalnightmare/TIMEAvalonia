using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using TIME.App.View.UserControls;
using TIME.Library.Interfaces;

namespace TIME.App.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IWindowService _windowService;

    public MainWindowViewModel(IWindowService windowService)
    {
        _windowService = windowService;
    }

    [ObservableProperty]
    private UserControl? currentView;

    [ObservableProperty]
    private string appName = "Titan IME";

    [RelayCommand]
    public void ShowMain()
    {
        CurrentView = new MainUserControl();
    }

    [RelayCommand]
    public void ShowRandomOne()
    {
        CurrentView = new RandomOneUserControl();
    }


    // View Commands
    [RelayCommand]
    public void MinimizeView() => _windowService.Minimize();

    [RelayCommand]
    public void MaximizeView() => _windowService.ToggleMaximize();

    [RelayCommand]
    public void CloseView() => _windowService.Close();
}
