namespace ExpenseManager.BusinessLayer.Models;

public enum TransactionCategory
{
    MedicalServices,
    Mobile,
    Groceries,
    Transfers,
    DigitalGoods,
    Transport,
    Charity,
    Bills,
    Entertainment,
    Restaurants,
    Withdrawals,
    Others,
}

public class Transaction
{
    private Guid _guid;
    private Guid _walletGuid;
    private decimal _amount;
    private TransactionCategory _category;
    private string _description;
    private DateTime _date;

    public Guid Guid
    {
        get { return _guid; }
        set { _guid = value; }
    }

    public Guid WalletGuid
    {
        get { return _walletGuid; }
        set { _walletGuid = value; }
    }

    public decimal Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public TransactionCategory Category
    {
        get { return _category; }
        set { _category = value; }
    }

    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }

    public DateTime Date
    {
        get { return _date; }
        set { _date = value; }
    }
    
    // for when retrieving an object from a storage
    public Transaction(Guid guid, Guid walletGuid, decimal amount, TransactionCategory category, string description, DateTime date)
    {
        Guid = guid;
        WalletGuid = walletGuid;
        Amount = amount;
        Category = category;
        Description = description;
        Date = date;
    }
    
    // for when creating an object in a service
    public Transaction(Guid walletGuid, decimal amount, TransactionCategory category, string description) : 
        this(Guid.NewGuid(), walletGuid, amount, category, description, DateTime.UtcNow)
    {
    }
}