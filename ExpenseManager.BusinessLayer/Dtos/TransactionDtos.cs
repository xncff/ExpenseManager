using ExpenseManager.BusinessLayer.Models;

namespace ExpenseManager.BusinessLayer.Dtos;

public class CreateTransactionRequest
{
    public Guid WalletGuid { get; }
    public decimal Amount { get; }
    public TransactionCategory Category { get; }
    public string Description { get; }

    public CreateTransactionRequest(Guid walletGuid, decimal amount, TransactionCategory category, string description)
    {
        WalletGuid = walletGuid;
        Amount = amount;
        Category = category;
        Description = description;
    }
}

public class GetTransactionRequest
{
    public Guid Guid { get; }

    public GetTransactionRequest(Guid guid)
    {
        Guid = guid;
    }
}

public class GetTransactionByWalletRequest
{
    public Guid WalletGuid { get; }

    public GetTransactionByWalletRequest(Guid walletGuid)
    {
        WalletGuid = walletGuid;
    }
}

public class UpdateTransactionRequest
{
    public Guid Guid { get; }
    public decimal Amount { get; }
    public TransactionCategory Category { get; }
    public string Description { get; }
    public DateTime Date { get; }

    public UpdateTransactionRequest(Guid guid, decimal amount, TransactionCategory category, string description, DateTime date)
    {
        Guid = guid;
        Amount = amount;
        Category = category;
        Description = description;
        Date = date;
    }
}

public class DeleteTransactionRequest
{
    public Guid Guid { get; }

    public DeleteTransactionRequest(Guid guid)
    {
        Guid = guid;
    }
}

public class TransactionResponse
{
    public Guid Guid { get; }
    public Guid WalletGuid { get; }
    public decimal Amount { get; }
    public TransactionCategory Category { get; }
    public string Description { get; }
    public DateTime Date { get; }

    public TransactionResponse(
        Guid guid,
        Guid walletGuid,
        decimal amount,
        TransactionCategory category,
        string description,
        DateTime date
    )
    {
        Guid = guid;
        WalletGuid = walletGuid;
        Amount = amount;
        Category = category;
        Description = description;
        Date = date;
    }
    
    public override string ToString()
    {
        return $"{Amount} at {Date}";
    }

    public string DescriptionForUI
    {
        get
        {
            return Description.IsWhiteSpace() ? "-" : Description;
        }
    }
}