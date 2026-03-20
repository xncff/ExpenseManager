using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;

namespace ExpenseManager.PresentationLayer.ViewModels;

public class TransactionDetailsViewModel : IQueryAttributable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;

    private TransactionResponse _transaction;
    private WalletResponse _wallet;
    private string _amountText;
    private Color _amountColor;

    public TransactionDetailsViewModel(ITransactionService transactionService, IWalletService walletService)
    {
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    public TransactionResponse Transaction
    {
        get => _transaction;
        private set { _transaction = value; OnPropertyChanged(); }
    }

    public string AmountText
    {
        get => _amountText;
        private set { _amountText = value; OnPropertyChanged(); }
    }

    public Color AmountColor
    {
        get => _amountColor;
        private set { _amountColor = value; OnPropertyChanged(); }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Guid transactionGuid = (Guid)query["TransactionGuid"];
        Guid walletGuid = (Guid)query["WalletGuid"];

        Transaction = _transactionService.GetByGuid(new GetTransactionRequest(transactionGuid));
        _wallet = _walletService.GetByGuid(new GetWalletRequest(walletGuid));

        AmountText = $"{_transaction.Amount} {_wallet.Currency}";
        AmountColor = _transaction.IsExpense ? Colors.Red : Colors.Green;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}