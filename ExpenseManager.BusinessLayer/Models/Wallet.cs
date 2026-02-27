namespace ExpenseManager.BusinessLayer.Models;

public enum Currency
{
    UAH,
    USD,
    EUR,
}

public class Wallet
{
    private Guid _guid;
    private string _name;
    private Currency _currency;
    
    public Guid Guid
    {
        get { return _guid; }
        set { _guid = value; }
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
    
    // for when retrieving an object from a storage
    public Wallet(Guid guid, string name, Currency currency)
    {
        Guid = guid;
        Name = name;
        Currency = currency;
    }
    
    // for when creating an object in a service
    public Wallet(string name, Currency currency) : this(Guid.NewGuid(), name, currency)
    {
    }
}