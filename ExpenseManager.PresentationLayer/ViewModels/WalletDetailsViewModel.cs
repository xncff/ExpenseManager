using System.Collections.ObjectModel;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.PresentationLayer.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class WalletDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    [ObservableProperty]
    private WalletResponse _currentWallet;
    [ObservableProperty]
    private ObservableCollection<TransactionResponse> _transactions;
    [ObservableProperty]
    private string _walletTotalText;

    public WalletDetailsViewModel(IWalletService walletService, ITransactionService transactionService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
    }
    
    [RelayCommand]
    private void LoadTransaction(Guid transactionGuid)
    {
        Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}",
            new Dictionary<string, object>
            {
                { "TransactionGuid", transactionGuid },
                { "WalletGuid", CurrentWallet.Guid }
            });
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Guid walletGuid = (Guid)query["WalletGuid"];
        CurrentWallet = _walletService.GetByGuid(new GetWalletRequest(walletGuid));
        
        var list = _transactionService.GetAllByWallet(
            new GetTransactionsByWalletRequest(walletGuid)
        ).ToList();
        Transactions = new ObservableCollection<TransactionResponse>(list);

        decimal total = _walletService.GetTotal(new GetWalletTotalRequest(walletGuid));
        WalletTotalText = $"Total expenses and incomes: {total} {CurrentWallet.Currency}";
    }
}
