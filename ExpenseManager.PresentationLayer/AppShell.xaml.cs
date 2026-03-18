using ExpenseManager.PresentationLayer.Pages;

namespace ExpenseManager.PresentationLayer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute($"{nameof(WalletsPage)}/{nameof(WalletDetailsPage)}", typeof(WalletDetailsPage));
        Routing.RegisterRoute(
            $"{nameof(WalletsPage)}/{nameof(WalletDetailsPage)}/{nameof(TransactionDetailsPage)}", 
            typeof(TransactionDetailsPage)
        );
    }
}