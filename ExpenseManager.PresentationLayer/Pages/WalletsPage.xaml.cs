using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class WalletsPage : ContentPage
{
    public WalletsPage(WalletsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}