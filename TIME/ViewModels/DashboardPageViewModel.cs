using TIME.Data;

namespace TIME.ViewModels;

public partial class DashboardPageViewModel() : PageViewModel(ApplicationPageNames.Dashboard)
{
    public string Test { get; set; } = "Dashboard";
}
