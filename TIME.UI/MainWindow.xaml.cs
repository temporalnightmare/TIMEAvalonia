using System.Windows;
using TIME.UI.ViewModels;

namespace TIME.UI;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}