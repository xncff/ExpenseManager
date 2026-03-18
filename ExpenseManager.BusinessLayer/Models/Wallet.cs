namespace ExpenseManager.BusinessLayer.Models;

public enum Currency
{
    UAH,
    USD,
    EUR,
}

public class Wallet
{
    public Guid Guid { get; }
    public string Name { get; set; }
    public Currency Currency { get; set; }
    
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