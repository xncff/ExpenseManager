using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class TransactionDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;
    
    [ObservableProperty]
    private TransactionResponse _transaction;
    private WalletResponse _wallet;
    [ObservableProperty]
    private string _amountText;
    [ObservableProperty]
    private Color _amountColor;

    public TransactionDetailsViewModel(ITransactionService transactionService, IWalletService walletService)
    {
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Guid transactionGuid = (Guid)query["TransactionGuid"];
        Guid walletGuid = (Guid)query["WalletGuid"];

        Transaction = _transactionService.GetByGuid(new GetTransactionRequest(transactionGuid));
        _wallet = _walletService.GetByGuid(new GetWalletRequest(walletGuid));

        AmountText = $"{Transaction.Amount} {_wallet.Currency}";
        AmountColor = Transaction.IsExpense ? Colors.Red : Colors.Green;
    }
}