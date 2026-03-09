using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;

namespace ExpenseManager.PresentationLayer.Pages;

[QueryProperty(nameof(CurrentTransaction), "transactionGuid")]
[QueryProperty(nameof(CurrentWallet), "walletGuid")]
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

    public string CurrentTransaction
    {
        set
        {
            GetTransactionRequest request = new GetTransactionRequest
            {
                Guid = Guid.Parse(value)
            };
            _transaction = _transactionService.GetByGuid(request);
        }
    }

    public string CurrentWallet
    {
        set
        {
            GetWalletRequest request = new GetWalletRequest
            {
                Guid = Guid.Parse(value)
            };
            _wallet = _walletService.GetByGuid(request);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        AmountLabel.Text = $"{_transaction.Amount} {_wallet.Currency}";
        AmountLabel.TextColor = (_transaction.Amount >= 0) ? Colors.Green : Colors.Red;

        CategoryLabel.Text = _transaction.Category.ToString();
        DescriptionLabel.Text = _transaction.DescriptionForUI;
        GuidLabel.Text = _transaction.Guid.ToString();
    }
}