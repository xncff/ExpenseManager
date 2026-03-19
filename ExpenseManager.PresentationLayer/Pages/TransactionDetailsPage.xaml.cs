using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;

namespace ExpenseManager.PresentationLayer.Pages;

[QueryProperty(nameof(CurrentTransactionGuid), "transactionGuid")]
[QueryProperty(nameof(CurrentWalletGuid), "walletGuid")]
public partial class TransactionDetailsPage : ContentPage
{
    private readonly ITransactionService _transactionService;
    private readonly IWalletService _walletService;

    private TransactionResponse _transaction;
    private WalletResponse _wallet;

    public TransactionDetailsPage(ITransactionService transactionService, IWalletService walletService)
    {
        InitializeComponent();
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }

    public string CurrentTransactionGuid
    {
        set
        {
            GetTransactionRequest request = new GetTransactionRequest(Guid.Parse(value));
            _transaction = _transactionService.GetByGuid(request);
        }
    }

    public string CurrentWalletGuid
    {
        set
        {
            GetWalletRequest request = new GetWalletRequest(Guid.Parse(value));
            _wallet = _walletService.GetByGuid(request);
        }
    }

    public TransactionResponse CurrentTransaction
    {
        get { return _transaction; }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        BindingContext = _transaction;
        AmountLabel.Text = $"{_transaction.Amount} {_wallet.Currency}";
        AmountLabel.TextColor = _transaction.IsExpense ? Colors.Red : Colors.Green;
    }
}