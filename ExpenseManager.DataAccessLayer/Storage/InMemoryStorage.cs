using ExpenseManager.DataAccessLayer.Models;

namespace ExpenseManager.DataAccessLayer.Storage;

public class InMemoryStorage
{
    public record WalletRecord(Guid Guid, string Name, Currency Currency);
    public record TransactionRecord(
        Guid Guid, 
        Guid WalletGuid, 
        decimal Amount, 
        TransactionCategory Category, 
        string Description, 
        DateTime Date
    );
    
    private readonly List<WalletRecord> _wallets;
    private readonly List<TransactionRecord> _transactions;

    public List<WalletRecord> Wallets => _wallets.ToList();
    public List<TransactionRecord> Transactions => _transactions.ToList();
    
    public InMemoryStorage()
    {
        _wallets = new List<WalletRecord>();
        _transactions = new List<TransactionRecord>();

        FillDb();
    }

    private void FillDb()
    {
        var mainWalletId = Guid.NewGuid();
        var savingsWalletId = Guid.NewGuid();
        var investWalletId = Guid.NewGuid();

        _wallets.Add(new WalletRecord(mainWalletId, "Зарплатня", Currency.UAH));
        _wallets.Add(new WalletRecord(savingsWalletId, "Скарбничка", Currency.UAH));
        _wallets.Add(new WalletRecord(investWalletId, "Інвестиції", Currency.USD));

        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -500, TransactionCategory.Groceries, "Продукти в Сільпо", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -85, TransactionCategory.Restaurants, "Кава", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -500, TransactionCategory.Bills, "Оплата електроенергії", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -100, TransactionCategory.Bills, "Оплата інтернету", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -400, TransactionCategory.DigitalGoods, "Підписка Netflix", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -250, TransactionCategory.Transport, "Таксі Uklon", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -600, TransactionCategory.MedicalServices, "Аптека Доброго Дня", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -400, TransactionCategory.Entertainment, "Квитки в кіно", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, 20000, TransactionCategory.Transfers, "Зарплатня", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), mainWalletId, -250, TransactionCategory.Mobile, "Поповнення тарифу", DateTime.UtcNow));

        _transactions.Add(new TransactionRecord(Guid.NewGuid(), savingsWalletId, 10000, TransactionCategory.Transfers, "", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), savingsWalletId, -12000, TransactionCategory.Transfers, "Оренда квартири", DateTime.UtcNow));
        _transactions.Add(new TransactionRecord(Guid.NewGuid(), savingsWalletId, -6000, TransactionCategory.MedicalServices, "Стоматолог", DateTime.UtcNow));
    }
}