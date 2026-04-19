using System.Text.Json;
using ExpenseManager.DataAccessLayer.Models;
using Microsoft.Maui.Storage;

namespace ExpenseManager.DataAccessLayer.Storage;

public class FileStorage : IStorage
{
    private static readonly string BasePath = Path.Combine(FileSystem.AppDataDirectory, "ExpenseManagerStorage");

    private async Task Init()
    {
        if (Directory.Exists(BasePath))
        {
            return;
        }
        await CreateMockStorage();
    }

    private async Task CreateMockStorage()
    {
        Directory.CreateDirectory(BasePath);

        Guid mainWalletId = Guid.NewGuid();
        Guid savingsWalletId = Guid.NewGuid();
        Guid investWalletId = Guid.NewGuid();

        WalletDbModel[] wallets =
        {
            new WalletDbModel(mainWalletId, "Зарплатня", Currency.UAH),
            new WalletDbModel(savingsWalletId, "Скарбничка", Currency.UAH),
            new WalletDbModel(investWalletId, "Інвестиції", Currency.USD),
        };

        TransactionDbModel[] transactions =
        {
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -500, TransactionCategory.Groceries, "Продукти в Сільпо", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -85, TransactionCategory.Restaurants, "Кава", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -500, TransactionCategory.Bills, "Оплата електроенергії", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -100, TransactionCategory.Bills, "Оплата інтернету", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -400, TransactionCategory.DigitalGoods, "Підписка Netflix", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -250, TransactionCategory.Transport, "Таксі Uklon", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -600, TransactionCategory.MedicalServices, "Аптека Доброго Дня", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -400, TransactionCategory.Entertainment, "Квитки в кіно", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, 20000, TransactionCategory.Transfers, "Зарплатня", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), mainWalletId, -250, TransactionCategory.Mobile, "Поповнення тарифу", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), savingsWalletId, 10000, TransactionCategory.Transfers, "", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), savingsWalletId, -12000, TransactionCategory.Transfers, "Оренда квартири", DateTime.UtcNow),
            new TransactionDbModel(Guid.NewGuid(), savingsWalletId, -6000, TransactionCategory.MedicalServices, "Стоматолог", DateTime.UtcNow),
        };

        List<Task> tasks = new List<Task>();
        foreach (WalletDbModel wallet in wallets)
        {
            Directory.CreateDirectory(WalletDirectoryPath(wallet.Guid));
            tasks.Add(File.WriteAllTextAsync(WalletFilePath(wallet.Guid), JsonSerializer.Serialize(wallet)));
        }
        foreach (TransactionDbModel tx in transactions)
        {
            tasks.Add(File.WriteAllTextAsync(TransactionFilePath(tx.WalletGuid, tx.Guid), JsonSerializer.Serialize(tx)));
        }

        await Task.WhenAll(tasks);
    }

    private string WalletFilePath(Guid walletGuid)
    {
        return Path.Combine(BasePath, walletGuid + ".json");
    }

    private string WalletDirectoryPath(Guid walletGuid)
    {
        return Path.Combine(BasePath, walletGuid.ToString());
    }

    private string TransactionFilePath(Guid walletGuid, Guid transactionGuid)
    {
        return Path.Combine(WalletDirectoryPath(walletGuid), transactionGuid + ".json");
    }

    public async IAsyncEnumerable<WalletDbModel> GetWalletsAsync()
    {
        await Init();
        foreach (string file in Directory.GetFiles(BasePath, "*.json"))
        {
            string json = await File.ReadAllTextAsync(file);
            WalletDbModel? wallet = JsonSerializer.Deserialize<WalletDbModel>(json);
            if (wallet is not null)
            {
                yield return wallet;
            }
        }
    }

    public async Task<WalletDbModel?> GetWalletAsync(Guid walletGuid)
    {
        await Init();
        string filePath = WalletFilePath(walletGuid);
        if (!File.Exists(filePath))
        {
            return null;
        }
        string json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<WalletDbModel>(json);
    }

    public async Task SaveWalletAsync(WalletDbModel wallet)
    {
        await Init();
        string directoryPath = WalletDirectoryPath(wallet.Guid);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        await File.WriteAllTextAsync(WalletFilePath(wallet.Guid), JsonSerializer.Serialize(wallet));
    }

    public async Task DeleteWalletAsync(Guid walletGuid)
    {
        await Init();
        string filePath = WalletFilePath(walletGuid);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        string directoryPath = WalletDirectoryPath(walletGuid);
        if (Directory.Exists(directoryPath))
        {
            Directory.Delete(directoryPath, recursive: true);
        }
    }

    public async Task<IEnumerable<TransactionDbModel>> GetTransactionsByWalletAsync(Guid walletGuid)
    {
        await Init();
        List<TransactionDbModel> transactions = new List<TransactionDbModel>();
        string directoryPath = WalletDirectoryPath(walletGuid);
        if (!Directory.Exists(directoryPath))
        {
            return transactions;
        }
        foreach (string file in Directory.GetFiles(directoryPath, "*.json"))
        {
            string json = await File.ReadAllTextAsync(file);
            TransactionDbModel? tx = JsonSerializer.Deserialize<TransactionDbModel>(json);
            if (tx is not null)
            {
                transactions.Add(tx);
            }
        }
        return transactions;
    }

    public async Task<TransactionDbModel?> GetTransactionAsync(Guid transactionGuid)
    {
        await Init();
        foreach (string directory in Directory.GetDirectories(BasePath))
        {
            string filePath = Path.Combine(directory, transactionGuid + ".json");
            if (!File.Exists(filePath))
            {
                continue;
            }
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<TransactionDbModel>(json);
        }
        return null;
    }

    public async Task SaveTransactionAsync(TransactionDbModel transaction)
    {
        await Init();
        string directoryPath = WalletDirectoryPath(transaction.WalletGuid);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        string filePath = TransactionFilePath(transaction.WalletGuid, transaction.Guid);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(transaction));
    }

    public async Task DeleteTransactionAsync(Guid transactionGuid)
    {
        await Init();
        foreach (string directory in Directory.GetDirectories(BasePath))
        {
            string filePath = Path.Combine(directory, transactionGuid + ".json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return;
            }
        }
    }
}
