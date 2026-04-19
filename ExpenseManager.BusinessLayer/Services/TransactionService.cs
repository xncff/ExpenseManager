using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.DataAccessLayer.Models;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Mappings;
using ExpenseManager.DataAccessLayer.Interfaces;

namespace ExpenseManager.BusinessLayer.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepo _repo;

    public TransactionService(ITransactionRepo repo)
    {
        _repo = repo;
    }

    public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request)
    {
        Transaction transaction = new Transaction(request.WalletGuid, request.Amount, request.Category, request.Description);
        await _repo.SaveAsync(transaction);
        return transaction.ToDto();
    }

    public async Task<TransactionResponse> GetByGuidAsync(GetTransactionRequest request)
    {
        Transaction transaction = await _repo.GetByGuidAsync(request.Guid)
            ?? throw new KeyNotFoundException($"Transaction {request.Guid} not found.");
        return transaction.ToDto();
    }

    public async Task<IEnumerable<TransactionResponse>> GetAllByWalletAsync(GetTransactionsByWalletRequest request)
    {
        IEnumerable<Transaction> transactions = await _repo.GetAllByWalletAsync(request.WalletGuid);
        return transactions.Select(t => t.ToDto()).ToList();
    }

    public async Task<TransactionResponse> UpdateAsync(UpdateTransactionRequest request)
    {
        Transaction transaction = await _repo.GetByGuidAsync(request.Guid)
            ?? throw new KeyNotFoundException($"Transaction {request.Guid} not found.");
        transaction.Amount = request.Amount;
        transaction.Category = request.Category;
        transaction.Description = request.Description;
        await _repo.SaveAsync(transaction);
        return transaction.ToDto();
    }

    public Task DeleteAsync(DeleteTransactionRequest request)
    {
        return _repo.DeleteAsync(request.Guid);
    }
}
