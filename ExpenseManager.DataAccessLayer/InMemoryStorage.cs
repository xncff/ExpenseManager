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
        var savingsWallet = new Wallet("Скарбничка", Currency.USD);
        var investWallet = new Wallet("Інвестиції", Currency.EUR);

        _wallets.Add(mainWallet);
        _wallets.Add(savingsWallet);
        _wallets.Add(investWallet);
        
        _transactions.Add(new Transaction(mainWallet.Guid, 500, ExpenseType.Groceries, "Продукти в Сільпо"));
        _transactions.Add(new Transaction(mainWallet.Guid, 85, ExpenseType.Restaurants, "Кава"));
        _transactions.Add(new Transaction(mainWallet.Guid, 500, ExpenseType.Bills, "Оплата електроенергії"));
        _transactions.Add(new Transaction(mainWallet.Guid, 100, ExpenseType.Bills, "Оплата інтернету"));
        _transactions.Add(new Transaction(mainWallet.Guid, 400, ExpenseType.DigitalGoods, "Підписка Netflix"));
        _transactions.Add(new Transaction(mainWallet.Guid, 250, ExpenseType.Transport, "Таксі Uklon"));
        _transactions.Add(new Transaction(mainWallet.Guid, 600, ExpenseType.MedicalServices, "Аптека Доброго Дня"));
        _transactions.Add(new Transaction(mainWallet.Guid, 400, ExpenseType.Entertainment, "Квитки в кіно"));
        _transactions.Add(new Transaction(mainWallet.Guid, 200, ExpenseType.DigitalGoods, "Курс на Coursera"));
        _transactions.Add(new Transaction(mainWallet.Guid, 250, ExpenseType.Mobile, "Поповнення тарифу"));
        
        _transactions.Add(new Transaction(savingsWallet.Guid, 12000, ExpenseType.Transfers, "Оренда квартири"));
        _transactions.Add(new Transaction(savingsWallet.Guid, 6000, ExpenseType.MedicalServices, "Стоматолог"));
    }
}