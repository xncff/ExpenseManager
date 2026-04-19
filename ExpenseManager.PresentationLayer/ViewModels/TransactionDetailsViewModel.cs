using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class TransactionDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;

    private Guid _transactionGuid;
    private Guid _walletGuid;
    private Currency _walletCurrency;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsExisting))]
    private bool _isNew;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReadMode))]
    private bool _isEditMode;

    [ObservableProperty]
    private decimal _amount;

    [ObservableProperty]
    private string _amountInput = "0";

    [ObservableProperty]
    private TransactionCategory _category;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private DateTime _date;

    [ObservableProperty]
    private string _amountText = string.Empty;

    [ObservableProperty]
    private Color _amountColor = Colors.Black;

    public bool IsExisting => !IsNew;
    public bool IsReadMode => !IsEditMode;

    public IReadOnlyList<TransactionCategory> AvailableCategories { get; } =
        Enum.GetValues(typeof(TransactionCategory)).Cast<TransactionCategory>().ToList();

    public TransactionDetailsViewModel(ITransactionService transactionService, IWalletService walletService)
    {
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _transactionGuid = (Guid)query["TransactionGuid"];
        _walletGuid = (Guid)query["WalletGuid"];

        IsNew = _transactionGuid == Guid.Empty;
        IsEditMode = IsNew;
    }

    [RelayCommand]
    public async Task Refresh()
    {
        IsBusy = true;
        try
        {
            WalletResponse wallet = _walletService.GetByGuid(new GetWalletRequest(_walletGuid));
            _walletCurrency = wallet.Currency;

            if (IsNew)
            {
                Amount = 0;
                Category = TransactionCategory.Others;
                Description = string.Empty;
                Date = DateTime.UtcNow;
            }
            else
            {
                TransactionResponse tx = _transactionService.GetByGuid(new GetTransactionRequest(_transactionGuid));
                Amount = tx.Amount;
                Category = tx.Category;
                Description = tx.Description;
                Date = tx.Date;
            }

            AmountInput = Amount.ToString(System.Globalization.CultureInfo.InvariantCulture);
            UpdateDisplay();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to load transaction: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateDisplay()
    {
        AmountText = $"{Amount} {_walletCurrency}";
        AmountColor = Amount < 0 ? Colors.Red : Colors.Green;
    }

    [RelayCommand]
    private void ToggleEdit()
    {
        if (!IsEditMode)
        {
            AmountInput = Amount.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
        IsEditMode = !IsEditMode;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!decimal.TryParse(AmountInput, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out decimal parsedAmount))
        {
            await Shell.Current.DisplayAlertAsync("Validation", "Amount must be a number.", "OK");
            return;
        }

        Amount = parsedAmount;
        string description = Description ?? string.Empty;

        IsBusy = true;
        try
        {
            if (IsNew)
            {
                TransactionResponse created = _transactionService.Create(
                    new CreateTransactionRequest(_walletGuid, Amount, Category, description));
                _transactionGuid = created.Guid;
                Date = created.Date;
                IsNew = false;
            }
            else
            {
                _transactionService.Update(new UpdateTransactionRequest(_transactionGuid, Amount, Category, description));
            }

            IsEditMode = false;
            UpdateDisplay();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Error", $"Failed to save transaction: {ex.Message}", "OK");
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
}
