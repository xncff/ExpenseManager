using ExpenseManager.DataAccessLayer;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.PresentationLayer.Pages;
using Microsoft.Extensions.Logging;

namespace ExpenseManager.PresentationLayer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        
        builder.Services.AddSingleton<InMemoryStorage>();
        
        builder.Services.AddTransient<ITransactionRepo, TransactionRepo>();
        builder.Services.AddTransient<IWalletRepo, WalletRepo>();
        
        builder.Services.AddTransient<TransactionService>();
        builder.Services.AddTransient<WalletService>();
        
        builder.Services.AddTransient<WalletsPage>();

        return builder.Build();
    }
}