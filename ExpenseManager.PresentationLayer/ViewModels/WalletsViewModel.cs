using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer.ViewModels;

public class WalletsViewModel
{
    private readonly IWalletService _walletService;
    
    private WalletResponse _selectedWallet;
    
    public ObservableCollection<WalletResponse> Wallets { get; set; }

    public WalletsViewModel(IWalletService walletService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        
        Wallets =  new ObservableCollection<WalletResponse>(_walletService.GetAll());
    }
    
    public WalletResponse SelectedWallet
    {
        get => _selectedWallet;
        set
        {
            _selectedWallet = value;
            if (value is not null)
            {
                Shell.Current.GoToAsync($"{nameof(WalletDetailsPage)}",
                    new Dictionary<string, object> { { "WalletGuid", value.Guid } });
            }
        }
    }
}
