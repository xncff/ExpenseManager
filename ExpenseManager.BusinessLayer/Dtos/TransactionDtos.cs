using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Dtos;

public class CreateTransactionRequest
{
    public Guid WalletGuid { get; set; }
    public decimal Amount { get; set; }
    public TransactionCategory Category { get; set; }
    public string Description { get; set; }
}

public class GetTransactionRequest
{
    public Guid Guid { get; set; }
}

public class GetTransactionByWalletRequest
{
    public Guid WalletGuid { get; set; }
}

public class UpdateTransactionRequest
{
    public Guid Guid { get; set; }
    public decimal Amount { get; set; }
    public TransactionCategory Category { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}

public class DeleteTransactionRequest
{
    public Guid Guid { get; set; }
}

public class TransactionResponse
{
    public Guid Guid { get; set; }
    public Guid WalletGuid { get; set; }
    public decimal Amount { get; set; }
    public TransactionCategory Category { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    
    public override string ToString()
    {
        return $"{Amount} at {Date}: {Description}";
    }
}