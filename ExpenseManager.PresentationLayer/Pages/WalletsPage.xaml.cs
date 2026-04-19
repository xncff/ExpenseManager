using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class WalletsPage : ContentPage
{
    private readonly WalletsViewModel _viewModel;

    public WalletsPage(WalletsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Reload();
    }

    private async void OnFilterChanged(object? sender, EventArgs e)
    {
        await _viewModel.Reload();
    }
}
