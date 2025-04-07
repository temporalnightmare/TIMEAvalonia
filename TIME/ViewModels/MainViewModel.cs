using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using TIME.Data;
using TIME.Factories;

namespace TIME.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly PageFactory _pageFactory;

    [ObservableProperty]
    public string _entryDashboard = "Dashboard";

    [ObservableProperty]
    private bool _sideMenuExpanded = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DashboardPageIsActive))]
    [NotifyPropertyChangedFor(nameof(HashGeneratorPageIsActive))]
    private PageViewModel _currentPage;

    [RelayCommand]
    private void SideMenuResize() => SideMenuExpanded = !SideMenuExpanded;

    [RelayCommand]
    private void GoToDashboard() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.Dashboard);

    [RelayCommand]
    private void GoToHashGenerator() => CurrentPage = _pageFactory.GetPageViewModel(ApplicationPageNames.HashGenerator);

    public bool DashboardPageIsActive => CurrentPage.PageName == ApplicationPageNames.Dashboard;
    public bool HashGeneratorPageIsActive => CurrentPage.PageName == ApplicationPageNames.HashGenerator;

    public MainViewModel(PageFactory pageFactory)
    {
        _pageFactory = pageFactory ?? throw new ArgumentNullException(nameof(pageFactory));
    }
}
