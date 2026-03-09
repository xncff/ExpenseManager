using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute("WalletsPage/WalletDetailsPage", typeof(WalletDetailsPage));
        Routing.RegisterRoute("WalletsPage/WalletDetailsPage/TransactionDetailsPage", typeof(TransactionDetailsPage));
    }
}