namespace ExpenseManager.BusinessLayer.Models;

public enum ExpenseType
{
    MedicalServices,
    Mobile,
    Groceries,
    Transfers,
    DigitalGoods,
    Transport,
    Charity,
}

public class Transaction
{
    private Guid _guid;
    private Guid _walletGuid;
    private decimal _amount;
    private ExpenseType _expenseType;
    private string _description;
    private DateTime _date;

    public Guid Guid
    {
        get { return _guid; }
        private  set { _guid = value; }
    }

    public Guid WalletGuid
    {
        get { return _walletGuid; }
        private set { _walletGuid = value; }
    }

    public decimal Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    public ExpenseType ExpenseType
    {
        get { return _expenseType; }
        set { _expenseType = value; }
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
    
    // if false, ignore ExpenseType
    // public bool IsExpense
    // {
    //     get { return Amount > 0; }
    // }
    
    public Transaction(Guid guid, Guid walletGuid, decimal amount, ExpenseType expenseType, string description, DateTime date)
    {
        Guid = guid;
        WalletGuid = walletGuid;
        Amount = amount;
        ExpenseType = expenseType;
        Description = description;
        Date = date;
    }

    public Transaction(Guid walletGuid, decimal amount, ExpenseType expenseType, string description)
    {
        Guid = Guid.NewGuid();
        WalletGuid = walletGuid;
        Amount = amount;
        ExpenseType = expenseType;
        Description = description;
        Date = DateTime.UtcNow;
    }
}