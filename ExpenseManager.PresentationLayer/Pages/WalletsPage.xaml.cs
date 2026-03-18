using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;

namespace ExpenseManager.PresentationLayer.Pages;

public partial class WalletsPage : ContentPage
{
    private readonly IWalletService _walletService;

    public WalletsPage(IWalletService walletService)
    {
        InitializeComponent();
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        List<WalletResponse> wallets = _walletService.GetAll().ToList();
        WalletsCollection.ItemsSource = wallets;
    }

    private void OnWalletSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not WalletResponse wallet)
        {
            return;
        }
        
        WalletsCollection.SelectedItem = null;
        
        Shell.Current.GoToAsync($"{nameof(WalletDetailsPage)}?walletGuid={wallet.Guid}");
    }
}