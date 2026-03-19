using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.DataAccessLayer;

public class InMemoryStorage
{
    private readonly List<Wallet> _wallets;
    private readonly List<Transaction> _transactions;

    public IReadOnlyList<Wallet> Wallets => _wallets;
    public IReadOnlyList<Transaction> Transactions => _transactions;
    
    public InMemoryStorage()
    {
        _wallets = new List<Wallet>();
        _transactions = new List<Transaction>();

        FillDb();
    }

    private void FillDb()
    {
        Wallet mainWallet = new Wallet("Зарплатня", Currency.UAH);
        Wallet savingsWallet = new Wallet("Скарбничка", Currency.UAH);
        Wallet investWallet = new Wallet("Інвестиції", Currency.USD);

        _wallets.Add(mainWallet);
        _wallets.Add(savingsWallet);
        _wallets.Add(investWallet);
        
        _transactions.Add(new Transaction(mainWallet.Guid, -500, TransactionCategory.Groceries, "Продукти в Сільпо"));
        _transactions.Add(new Transaction(mainWallet.Guid, -85, TransactionCategory.Restaurants, "Кава"));
        _transactions.Add(new Transaction(mainWallet.Guid, -500, TransactionCategory.Bills, "Оплата електроенергії"));
        _transactions.Add(new Transaction(mainWallet.Guid, -100, TransactionCategory.Bills, "Оплата інтернету"));
        _transactions.Add(new Transaction(mainWallet.Guid, -400, TransactionCategory.DigitalGoods, "Підписка Netflix"));
        _transactions.Add(new Transaction(mainWallet.Guid, -250, TransactionCategory.Transport, "Таксі Uklon"));
        _transactions.Add(new Transaction(mainWallet.Guid, -600, TransactionCategory.MedicalServices, "Аптека Доброго Дня"));
        _transactions.Add(new Transaction(mainWallet.Guid, -400, TransactionCategory.Entertainment, "Квитки в кіно"));
        _transactions.Add(new Transaction(mainWallet.Guid, 20000, TransactionCategory.Transfers, "Зарплатня"));
        _transactions.Add(new Transaction(mainWallet.Guid, -250, TransactionCategory.Mobile, "Поповнення тарифу"));
        
        _transactions.Add(new Transaction(savingsWallet.Guid, 10000, TransactionCategory.Transfers, ""));
        _transactions.Add(new Transaction(savingsWallet.Guid, -12000, TransactionCategory.Transfers, "Оренда квартири"));
        _transactions.Add(new Transaction(savingsWallet.Guid, -6000, TransactionCategory.MedicalServices, "Стоматолог"));
    }
}