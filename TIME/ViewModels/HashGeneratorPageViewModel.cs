using TIME.Data;

namespace TIME.ViewModels;

public partial class HashGeneratorPageViewModel() : PageViewModel(ApplicationPageNames.HashGenerator)
{
    public string Test { get; set; } = "Hash Generator";
}
