using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer.ViewModels;

public partial class WalletsViewModel
{
    private readonly IWalletService _walletService;
    
    public ObservableCollection<WalletResponse> Wallets { get; set; }

    public WalletsViewModel(IWalletService walletService)
    {
        _walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
        Wallets =  new ObservableCollection<WalletResponse>(_walletService.GetAll());
    }
    
    [RelayCommand]
    private void LoadWallet(Guid walletGuid)
    {
        Shell.Current.GoToAsync($"{nameof(WalletDetailsPage)}",
            new Dictionary<string, object> { { "WalletGuid", walletGuid } });
    }
}
