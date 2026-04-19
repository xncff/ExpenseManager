using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateAsync(CreateTransactionRequest request);
        Task<TransactionResponse> GetByGuidAsync(GetTransactionRequest request);
        Task<IEnumerable<TransactionResponse>> GetAllByWalletAsync(GetTransactionsByWalletRequest request);
        Task<TransactionResponse> UpdateAsync(UpdateTransactionRequest request);
        Task DeleteAsync(DeleteTransactionRequest request);
    }
}
