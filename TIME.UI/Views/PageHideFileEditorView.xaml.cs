using System.Windows.Controls;
using TIME.Library.Interfaces;
using TIME.UI.ViewModels;

namespace TIME.UI.Views;

public partial class PageHideFileEditorView : UserControl
{
    public PageHideFileEditorView(PageHideFileEditorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
