using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Mappings;

public static class TransactionMapper
{
    public static TransactionResponse ToDto(this Transaction transaction)
    {
        return new TransactionResponse
        {
            Guid = transaction.Guid,
            WalletGuid = transaction.WalletGuid,
            Amount = transaction.Amount,
            Category = transaction.Category,
            Description = transaction.Description,
            Date = transaction.Date
        };
    }
}