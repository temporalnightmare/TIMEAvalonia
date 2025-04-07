using System.Windows;
using System.Windows.Media;
using TIME.App.ViewModel;
using TIME.Library.Interfaces;

namespace TIME.App;

public partial class MainWindow : Window, IWindowService
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel(this);
    }
    public void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    public void Minimize() => WindowState = WindowState.Minimized;

    public void ToggleMaximize()
    {
        if (WindowState == WindowState.Maximized)
            WindowState = WindowState.Normal;
        else
            WindowState = WindowState.Maximized;
    }

    public void CloseAppWindow() => this.Close();

    void IWindowService.Close() => CloseAppWindow();
}