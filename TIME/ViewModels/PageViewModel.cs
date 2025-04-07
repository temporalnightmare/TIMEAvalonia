using CommunityToolkit.Mvvm.ComponentModel;
using TIME.Data;

namespace TIME.ViewModels;

public partial class PageViewModel : ViewModelBase
{
    [ObservableProperty]
    private ApplicationPageNames _pageName;

    protected PageViewModel(ApplicationPageNames pageName)
    {
        _pageName = pageName;

        // Detect design time
        if (Avalonia.Controls.Design.IsDesignMode)
            OnDesignTimeConstructor();

        
    }

    protected virtual void OnDesignTimeConstructor() { }
}
