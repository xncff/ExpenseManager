using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class WalletDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    private Guid _walletGuid;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsExisting))]
    private bool _isNew;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReadMode))]
    private bool _isEditMode;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private Currency _currency;

    [ObservableProperty]
    private string _walletTotalText = string.Empty;

    public bool IsExisting => !IsNew;
    public bool IsReadMode => !IsEditMode;

    public ObservableCollection<TransactionResponse> Transactions { get; } = new();

    public IReadOnlyList<Currency> AvailableCurrencies { get; } =
        Enum.GetValues(typeof(Currency)).Cast<Currency>().ToList();

    public IReadOnlyList<string> SortOptions { get; } = new[]
    {
        "Date (newest)",
        "Date (oldest)",
        "Amount (highest)",
        "Amount (lowest)",
    };

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _selectedSort;

    public WalletDetailsViewModel(IWalletService walletService, ITransactionService transactionService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _selectedSort = SortOptions[0];
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _walletGuid = (Guid)query["WalletGuid"];
        IsNew = _walletGuid == Guid.Empty;
        IsEditMode = IsNew;

        if (IsNew)
        {
            Name = string.Empty;
            Currency = Currency.UAH;
            Transactions.Clear();
            WalletTotalText = string.Empty;
        }
    }

    [RelayCommand]
    public async Task Refresh()
    {
        if (IsNew)
        {
            return;
        }

        IsBusy = true;
        try
        {
            WalletResponse wallet = await _walletService.GetByGuidAsync(new GetWalletRequest(_walletGuid));
            Name = wallet.Name;
            Currency = wallet.Currency;

            decimal total = await _walletService.GetTotalAsync(new GetWalletTotalRequest(_walletGuid));
            WalletTotalText = $"Total expenses and incomes: {total} {wallet.Currency}";

            await LoadTransactionsList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load wallet: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task ReloadTransactions()
    {
        if (_walletGuid == Guid.Empty)
        {
            return;
        }

        IsBusy = true;
        try
        {
            await LoadTransactionsList();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load transactions: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task LoadTransactionsList()
    {
        IEnumerable<TransactionResponse> items = await _transactionService.GetAllByWalletAsync(
            new GetTransactionsByWalletRequest(_walletGuid));

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string query = SearchText.Trim();
            items = items.Where(t => t.Description.Contains(query, StringComparison.OrdinalIgnoreCase));
        }

        items = SelectedSort switch
        {
            "Date (oldest)" => items.OrderBy(t => t.Date),
            "Amount (highest)" => items.OrderByDescending(t => t.Amount),
            "Amount (lowest)" => items.OrderBy(t => t.Amount),
            _ => items.OrderByDescending(t => t.Date),
        };

        Transactions.Clear();
        foreach (TransactionResponse tx in items)
        {
            Transactions.Add(tx);
        }
    }

    [RelayCommand]
    private void ToggleEdit() => IsEditMode = !IsEditMode;

    [RelayCommand]
    private async Task Save()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlertAsync("Validation", "Name cannot be empty.", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            if (IsNew)
            {
                WalletResponse created = await _walletService.CreateAsync(new CreateWalletRequest(Name.Trim(), Currency));
                _walletGuid = created.Guid;
                IsNew = false;
            }
            else
            {
                await _walletService.UpdateAsync(new UpdateWalletRequest(_walletGuid, Name.Trim(), Currency));
            }

            IsEditMode = false;
            await Refresh();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to save wallet: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        if (IsNew)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        IsEditMode = false;
        await Refresh();
    }

    [RelayCommand]
    private async Task LoadTransaction(Guid transactionGuid)
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync(nameof(TransactionDetailsPage),
                new Dictionary<string, object>
                {
                    { "TransactionGuid", transactionGuid },
                    { "WalletGuid", _walletGuid }
                });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to open transaction: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddTransaction()
    {
        if (IsNew)
        {
            return;
        }

        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync(nameof(TransactionDetailsPage),
                new Dictionary<string, object>
                {
                    { "TransactionGuid", Guid.Empty },
                    { "WalletGuid", _walletGuid }
                });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to open transaction creation: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task DeleteTransaction(Guid transactionGuid)
    {
        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Delete transaction",
            "Are you sure?",
            "Delete",
            "Cancel");

        if (!confirmed)
        {
            return;
        }

        IsBusy = true;
        try
        {
            await _transactionService.DeleteAsync(new DeleteTransactionRequest(transactionGuid));
            await Refresh();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to delete transaction: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
