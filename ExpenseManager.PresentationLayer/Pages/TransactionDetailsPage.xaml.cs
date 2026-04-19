using System.ComponentModel;
using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class TransactionDetailsPage : ContentPage
{
    private readonly TransactionDetailsViewModel _viewModel;

    private readonly ToolbarItem _editItem;
    private readonly ToolbarItem _saveItem;
    private readonly ToolbarItem _cancelItem;

    public TransactionDetailsPage(TransactionDetailsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        _editItem = new ToolbarItem { Text = "Edit", Command = viewModel.ToggleEditCommand };
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

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TransactionDetailsViewModel.IsEditMode))
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
    }
}
