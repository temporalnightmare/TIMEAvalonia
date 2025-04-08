using CommunityToolkit.Mvvm.ComponentModel;

namespace TIME.UI.Models;

public partial class FlagItem : ObservableObject
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private int _position;
    [ObservableProperty] private string _category;
    [ObservableProperty] private string _subCategory;
    [ObservableProperty] private int _defaultValue;
    [ObservableProperty] private int _unlockedValue;
    [ObservableProperty] private bool _isChecked;
    public FlagItem(string name, int position, string category, int defaultValue, int unlockedValue)
    {
        Name = name;
        Position = position;
        Category = category;
        DefaultValue = defaultValue;
        UnlockedValue = unlockedValue;
        SubCategory = string.Empty;
        IsChecked = false;
    }
}
