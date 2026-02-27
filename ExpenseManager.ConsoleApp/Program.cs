using System.Text;
using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.DataAccessLayer;

namespace ExpenseManager.ConsoleApp;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        TransactionRepo transactionRepo = new TransactionRepo();
        WalletRepo walletRepo = new WalletRepo();

        TransactionService transactionService = new TransactionService(transactionRepo);
        WalletService walletService = new WalletService(walletRepo);
        
        MainLoop(walletService, transactionService);
    }

    static void MainLoop(WalletService walletService,  TransactionService transactionService)
    {
        bool exitApp = false;
        while (!exitApp)
        {
            Console.Clear();
            Console.WriteLine("=== Wallets ===");
            
            List<WalletResponse> wallets = walletService.GetAll().ToList();

            if (wallets.Count == 0)
            {
                Console.WriteLine("The storage is empty.");
            }
            else
            {
                for (int i = 0; i < wallets.Count; ++i)
                {
                    Console.WriteLine($"{i + 1}. {wallets[i]}");
                }
            }

            Console.WriteLine("\nEnter the wallet number for details or '0' to exit:");
            string input = Console.ReadLine();

            if (input == "0")
            {
                exitApp = true; 
                continue;
            }
            
            int walletIndex;
            if (int.TryParse(input, out walletIndex) && walletIndex > 0 && walletIndex <= wallets.Count)
            {
                WalletResponse selectedWallet = wallets[walletIndex - 1];
                ShowWalletDetails(selectedWallet, transactionService);
            }
            else
            {
                ShowInvalidInputMessage();
            }
        }
    }

    static void ShowWalletDetails(WalletResponse wallet, TransactionService transactionService)
    {
        int transNumToShow;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nEnter a number of transactions to show or '0' to go back:");
            string input = Console.ReadLine();

            if (input == "0")
            {
                return;
            }
            
            if (int.TryParse(input, out transNumToShow) && transNumToShow > 0)
            {
                break;
            }
            ShowInvalidInputMessage();
        }
        
        bool goBack = false;
        while (!goBack)
        {
            Console.Clear();
            Console.WriteLine("=== Wallet details ===");
            
            Console.WriteLine(wallet.ToString());
            Console.WriteLine("------------------------------------------------");
            
            GetTransactionByWalletRequest request = new GetTransactionByWalletRequest();
            request.WalletGuid = wallet.Guid;
            List<TransactionResponse> transactions = transactionService.GetAllByWallet(request).ToList();
            
            decimal total = 0;
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found for this wallet.");
            }
            else
            {
                Console.WriteLine("=== Transactions ===");
                for (int i = 0; i < transactions.Count; ++i)
                {
                    if (i < transNumToShow)
                    {
                        Console.WriteLine($"{i + 1}. {transactions[i]}");
                    }
                    total += transactions[i].Amount;
                }
            }
            Console.WriteLine($"\nTotal expenses and incomes: {total} {wallet.Currency}");

            Console.WriteLine("\nEnter the transaction number for full information or '0' to go back:");
            string input = Console.ReadLine();

            if (input == "0")
            {
                goBack = true; 
                continue;
            }
            
            int transactionIndex;
            if (int.TryParse(input, out transactionIndex) && 
                transactionIndex > 0 && 
                transactionIndex <= transactions.Count && 
                transactionIndex <= transNumToShow)
            {
                TransactionResponse selectedTransaction = transactions[transactionIndex - 1];
                ShowTransactionDetails(selectedTransaction, wallet); 
            }
            else
            {
                ShowInvalidInputMessage();
            }
        }
    }
    
    static void ShowTransactionDetails(TransactionResponse transaction, WalletResponse wallet) 
    {
        Console.Clear();
        Console.WriteLine("=== Transaction details ===");
        
        Console.WriteLine($"ID: {transaction.Guid}");
        string action = transaction.Amount >= 0 ? "received" : "spent";
        Console.WriteLine($"Amount {action}: {Math.Abs(transaction.Amount)} {wallet.Currency}"); 
        Console.WriteLine($"Category: {transaction.Category}");
        Console.WriteLine($"Description: {transaction.Description}");
        
        Console.WriteLine("\nPress any key to return to the transaction list...");
        Console.ReadKey();
    }
    
    static void ShowInvalidInputMessage()
    {
        Console.WriteLine("Invalid input. Press any key to continue...");
        Console.ReadKey();
    }
}