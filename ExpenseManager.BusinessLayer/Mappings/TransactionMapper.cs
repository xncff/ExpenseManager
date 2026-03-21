using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Mappings;

public static class TransactionMapper
{
    public static TransactionResponse ToDto(this Transaction transaction)
    {
        return new TransactionResponse(
            transaction.Guid,
            transaction.WalletGuid,
            transaction.Amount,
            transaction.Category,
            transaction.Description,
            transaction.Date
        );
    }
}