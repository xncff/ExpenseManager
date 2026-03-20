using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class WalletDetailsPage : ContentPage
{
    public WalletDetailsPage(WalletDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}