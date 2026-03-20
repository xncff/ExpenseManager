using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class TransactionDetailsPage : ContentPage
{
    public TransactionDetailsPage(TransactionDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}