namespace ExpenseManager.BusinessLayer.Models;

public enum Currency
{
    UAH,
    USD,
    EUR,
}

// for now I don't see any usage in making setters public
public class Wallet
{
    private Guid _guid;
    private string _name;
    private Currency _currency;
    
    public Guid Guid
    {
        get { return _guid; }
        private set { _guid = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Currency Currency
    {
        get { return _currency; }
        set { _currency = value; }
    }
    
    public Wallet(Guid guid, string name, Currency currency)
    {
        Guid = guid;
        Name = name;
        Currency = currency;
    }
    
    public Wallet(string name, Currency currency)
    {
        Guid = Guid.NewGuid();
        Name = name;
        Currency = currency;
    }
}