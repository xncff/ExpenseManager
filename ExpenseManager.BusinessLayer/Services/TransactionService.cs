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
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public TransactionResponse Create(CreateTransactionRequest request)
    {
        Transaction toCreate = new Transaction(request.WalletGuid, request.Amount, request.Category, request.Description);
        return _repo.Create(toCreate).ToDto();
    }

    public TransactionResponse GetByGuid(GetTransactionRequest request)
    {
        return _repo.GetByGuid(request.Guid).ToDto();
    }

    public IEnumerable<TransactionResponse> GetAllByWallet(GetTransactionsByWalletRequest request)
    {
        return _repo.GetAllByWallet(request.WalletGuid).Select(t => t.ToDto()).ToList();
    }

    public IEnumerable<TransactionResponse> GetAll()
    {
        return _repo.GetAll().Select(t => t.ToDto()).ToList();
    }

    public TransactionResponse Update(UpdateTransactionRequest request)
    {
        Transaction toUpdate = _repo.GetByGuid(request.Guid);
        toUpdate.Amount = request.Amount;
        toUpdate.Category = request.Category;
        toUpdate.Description = request.Description;
        return _repo.Update(toUpdate).ToDto();
    }

    public void Delete(DeleteTransactionRequest request)
    {
        _repo.Delete(request.Guid);
    }
}
