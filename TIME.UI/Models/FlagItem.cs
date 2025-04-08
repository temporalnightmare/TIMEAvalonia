using CommunityToolkit.Mvvm.ComponentModel;

namespace TIME.UI.Models;

public partial class FlagItem : ObservableObject
{
    [ObservableProperty]
    private bool _isChecked;

    [ObservableProperty]
    private string _name;

    public int Position { get; }
    public string Category { get; } = string.Empty;
    public byte UnlockedValue { get; } // Value when enabled
    public byte DefaultValue { get; }  // Value when disabled

    public FlagItem(string name, int position, string category, byte unlockedValue, byte defaultValue, bool initialState = false)
    {
        Name = name;
        Position = position;
        Category = category;
        UnlockedValue = unlockedValue;
        DefaultValue = defaultValue;
        IsChecked = false;
    }
}
