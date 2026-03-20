using ExpenseManager.DataAccessLayer;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.PresentationLayer.Pages;
using ExpenseManager.PresentationLayer.ViewModels;
using Microsoft.Extensions.Logging;

namespace ExpenseManager.PresentationLayer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();
        
        builder.Services.AddSingleton<InMemoryStorage>();
        
        builder.Services.AddSingleton<ITransactionRepo, TransactionRepo>();
        builder.Services.AddSingleton<IWalletRepo, WalletRepo>();
        
        builder.Services.AddSingleton<ITransactionService, TransactionService>();
        builder.Services.AddSingleton<IWalletService, WalletService>();
        
        builder.Services.AddTransient<WalletsPage>();
        builder.Services.AddTransient<WalletDetailsPage>();
        builder.Services.AddTransient<TransactionDetailsPage>();

        builder.Services.AddTransient<WalletsViewModel>();
        builder.Services.AddTransient<WalletDetailsViewModel>();
        builder.Services.AddTransient<TransactionDetailsViewModel>();

        return builder.Build();
    }
}