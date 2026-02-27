using System.Text;
using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Services;
using ExpenseManager.DataAccessLayer;

namespace ExpenseManager.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        TransactionRepo transactionRepo = new TransactionRepo();
        WalletRepo walletRepo = new WalletRepo();

        TransactionService transactionService = new TransactionService(transactionRepo);
        WalletService walletService = new WalletService(walletRepo);

        bool exitApp = false;
        while (!exitApp)
        {
            Console.Clear();
            Console.WriteLine("=== Wallets ===");
            
            List<Wallet> wallets = walletService.GetAll().ToList();

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
                Wallet selectedWallet = wallets[walletIndex - 1];
                ShowWalletDetails(selectedWallet, transactionService);
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to continue...");
                Console.ReadKey();
            }
        }
    }

    static void ShowWalletDetails(Wallet wallet, TransactionService transactionService)
    {
        bool goBack = false;
        while (!goBack)
        {
            Console.Clear();
            Console.WriteLine("=== Wallet details ===");
            
            Console.WriteLine(wallet.ToString());
            Console.WriteLine("------------------------------------------------");

            List<Transaction> transactions = transactionService.GetAllByWalletGuid(wallet.Guid).ToList();

            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found for this wallet.");
            }
            else
            {
                Console.WriteLine("=== Transactions ===");
                for (int i = 0; i < transactions.Count; ++i)
                {
                    Console.WriteLine($"{i + 1}. {transactions[i]}");
                }
            }

            Console.WriteLine("\nEnter the transaction number for full information or '0' to go back:");
            string input = Console.ReadLine();

            if (input == "0")
            {
                goBack = true; 
                continue;
            }
            
            int transactionIndex;
            if (int.TryParse(input, out transactionIndex) && transactionIndex > 0 && transactionIndex <= transactions.Count)
            {
                Transaction selectedTransaction = transactions[transactionIndex - 1];
                ShowTransactionDetails(selectedTransaction, wallet); 
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
    
    static void ShowTransactionDetails(Transaction transaction, Wallet wallet) 
    {
        Console.Clear();
        Console.WriteLine("=== Transaction details ===");
        
        Console.WriteLine($"ID: {transaction.Guid}");
        Console.WriteLine($"Amount: {transaction.Amount} {wallet.Currency}"); 
        Console.WriteLine($"Category: {transaction.ExpenseType}");
        Console.WriteLine($"Description: {transaction.Description}");
        
        Console.WriteLine("\nPress any key to return to the transaction list...");
        Console.ReadKey();
    }
}