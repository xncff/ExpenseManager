using ExpenseManager.DataAccessLayer.Storage;
using ExpenseManager.DataAccessLayer.Interfaces;
using ExpenseManager.DataAccessLayer.Repositories;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.PresentationLayer.Pages;
using ExpenseManager.PresentationLayer.ViewModels;

namespace ExpenseManager.PresentationLayer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();

        builder.Services.AddSingleton<IStorage, FileStorage>();
        
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