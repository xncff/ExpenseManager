using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.DataAccessLayer.Models;

public enum TransactionCategory
{
    [Display(Name = "Medical services")]
    MedicalServices,
    Mobile,
    Groceries,
    Transfers,
    [Display(Name = "Digital goods")]
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
    public Guid Guid { get; }
    public Guid WalletGuid { get; }
    public decimal Amount { get; set; }
    public TransactionCategory Category { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; }
    
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