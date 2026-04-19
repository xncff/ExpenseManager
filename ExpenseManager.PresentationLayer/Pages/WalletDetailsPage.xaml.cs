using System.ComponentModel;
using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class WalletDetailsPage : ContentPage
{
    private readonly WalletDetailsViewModel _viewModel;

    private readonly ToolbarItem _editItem;
    private readonly ToolbarItem _addItem;
    private readonly ToolbarItem _saveItem;
    private readonly ToolbarItem _cancelItem;

    public WalletDetailsPage(WalletDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        _editItem = new ToolbarItem { Text = "Edit", Command = viewModel.ToggleEditCommand };
        _addItem = new ToolbarItem { Text = "Add", Command = viewModel.AddTransactionCommand };
        _saveItem = new ToolbarItem { Text = "Save", Command = viewModel.SaveCommand };
        _cancelItem = new ToolbarItem { Text = "Cancel", Command = viewModel.CancelCommand };

        viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        SyncToolbar();
        await _viewModel.Refresh();
    }

    private async void OnFilterChanged(object? sender, EventArgs e)
    {
        await _viewModel.ReloadTransactions();
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(WalletDetailsViewModel.IsEditMode)
            or nameof(WalletDetailsViewModel.IsNew))
        {
            SyncToolbar();
        }
    }

    private void SyncToolbar()
    {
        ToolbarItems.Clear();

        if (_viewModel.IsEditMode)
        {
            ToolbarItems.Add(_saveItem);
            ToolbarItems.Add(_cancelItem);
            return;
        }

        ToolbarItems.Add(_editItem);
        if (_viewModel.IsExisting)
        {
            ToolbarItems.Add(_addItem);
        }
    }
}
