using System.Windows;
using TIME.UI.ViewModels;

namespace TIME.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}