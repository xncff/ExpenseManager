using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

internal static class InMemoryStorage
{
    private static readonly List<Wallet> _wallets;
    private static readonly List<Transaction> _transactions;

    internal static List<Wallet> Wallets
    {
        get { return _wallets.ToList(); }
    }
    
    internal static List<Transaction> Transactions
    {
        get { return _transactions.ToList(); }
    }
    
    static InMemoryStorage()
    {
        _wallets = new List<Wallet>();
        _transactions = new List<Transaction>();

        FillDB();
    }

    private static void FillDB()
    {
        var mainWallet = new Wallet("Зарплатня", Currency.UAH);
        var savingsWallet = new Wallet("Скарбничка", Currency.UAH);
        var investWallet = new Wallet("Інвестиції", Currency.USD);

        _wallets.Add(mainWallet);
        _wallets.Add(savingsWallet);
        _wallets.Add(investWallet);
        
        _transactions.Add(new Transaction(mainWallet.Guid, -500, TransCategory.Groceries, "Продукти в Сільпо"));
        _transactions.Add(new Transaction(mainWallet.Guid, -85, TransCategory.Restaurants, "Кава"));
        _transactions.Add(new Transaction(mainWallet.Guid, -500, TransCategory.Bills, "Оплата електроенергії"));
        _transactions.Add(new Transaction(mainWallet.Guid, -100, TransCategory.Bills, "Оплата інтернету"));
        _transactions.Add(new Transaction(mainWallet.Guid, -400, TransCategory.DigitalGoods, "Підписка Netflix"));
        _transactions.Add(new Transaction(mainWallet.Guid, -250, TransCategory.Transport, "Таксі Uklon"));
        _transactions.Add(new Transaction(mainWallet.Guid, -600, TransCategory.MedicalServices, "Аптека Доброго Дня"));
        _transactions.Add(new Transaction(mainWallet.Guid, -400, TransCategory.Entertainment, "Квитки в кіно"));
        _transactions.Add(new Transaction(mainWallet.Guid, 20000, TransCategory.Transfers, "Зарплатня"));
        _transactions.Add(new Transaction(mainWallet.Guid, -250, TransCategory.Mobile, "Поповнення тарифу"));
        
        _transactions.Add(new Transaction(savingsWallet.Guid, 10000, TransCategory.Transfers, ""));
        _transactions.Add(new Transaction(savingsWallet.Guid, -12000, TransCategory.Transfers, "Оренда квартири"));
        _transactions.Add(new Transaction(savingsWallet.Guid, -6000, TransCategory.MedicalServices, "Стоматолог"));
    }
}