using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;

namespace ExpenseManager.PresentationLayer.Pages;

[QueryProperty(nameof(CurrentWallet), "walletGuid")]
public partial class WalletDetailsPage : ContentPage
{
    private readonly IWalletService _walletService;
    private readonly ITransactionService _transactionService;

    private WalletResponse _wallet;
    private List<TransactionResponse> _transactions;

    public WalletDetailsPage(IWalletService walletService, ITransactionService transactionService)
    {
        InitializeComponent();
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
    }
    
    public string CurrentWallet
    {
        set
        {
            Guid guid = Guid.Parse(value);
            
            GetWalletRequest walletRequest = new GetWalletRequest(guid);
            _wallet = _walletService.GetByGuid(walletRequest);

            GetTransactionsByWalletRequest txRequest = new GetTransactionsByWalletRequest(guid);
            _transactions = _transactionService.GetAllByWallet(txRequest).ToList();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        WalletNameLabel.Text = $"{_wallet.Name}";
        
        WalletTotalLabel.Text = $"Total expenses and incomes: " +
                                $"{_walletService.GetTotal(new GetWalletTotalRequest(_wallet.Guid))} {_wallet.Currency}";

        TransactionsCollection.ItemsSource = _transactions;
    }

    private void OnTransactionSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not TransactionResponse transaction)
        {
            return;
        }

        TransactionsCollection.SelectedItem = null;

        Shell.Current.GoToAsync($"{nameof(TransactionDetailsPage)}?transactionGuid={transaction.Guid}&walletGuid={_wallet.Guid}");
    }
}