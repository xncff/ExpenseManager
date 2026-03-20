using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.PresentationLayer.Pages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class WalletDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    private WalletResponse _currentWallet;
    [ObservableProperty]
    private ObservableCollection<TransactionResponse> _transactions;
    private TransactionResponse _selectedTransaction;
    [ObservableProperty]
    private string _walletTotalText;

    public WalletDetailsViewModel(IWalletService walletService, ITransactionService transactionService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
    }
    
    public WalletResponse CurrentWallet
    {
        get => _currentWallet;
        private set
        {
            _currentWallet = value;

            var list = _transactionService.GetAllByWallet(
                new GetTransactionsByWalletRequest(_currentWallet.Guid)
            ).ToList();
            Transactions = new ObservableCollection<TransactionResponse>(list);

            decimal total = _walletService.GetTotal(new GetWalletTotalRequest(_currentWallet.Guid));
            WalletTotalText = $"Total expenses and incomes: {total} {_currentWallet.Currency}";
            
            OnPropertyChanged();
        }
    }
    
    public TransactionResponse SelectedTransaction
    {
        get => _selectedTransaction;
        set
        {
            _selectedTransaction = value;
            if (value is not null)
            {
                Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}",
                    new Dictionary<string, object>
                    {
                        { "TransactionGuid", value.Guid },
                        { "WalletGuid", CurrentWallet.Guid }
                    });
            }
        }
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Guid walletGuid = (Guid)query["WalletGuid"];
        CurrentWallet = _walletService.GetByGuid(new GetWalletRequest(walletGuid));
    }
}
