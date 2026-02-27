using ExpenseManager.BusinessLayer.Dtos;
using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Interfaces;
using ExpenseManager.BusinessLayer.Mappings;

namespace ExpenseManager.BusinessLayer.Services;

public class TransactionService
{
    private readonly ITransactionRepo _repo;

    public TransactionService(ITransactionRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public TransactionResponse Create(CreateTransactionRequest request)
    {
        try
        {
            Transaction toCreate = new Transaction(request.WalletGuid, request.Amount, request.Category, request.Description);
            return _repo.Create(toCreate).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public TransactionResponse GetByGuid(GetTransactionRequest request)
    {
        try
        {
            return _repo.GetByGuid(request.Guid).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<TransactionResponse> GetAllByWallet(GetTransactionByWalletRequest request)
    {
        try
        {
            List<TransactionResponse> result = new List<TransactionResponse>();
            foreach (Transaction transaction in _repo.GetAllByWallet(request.WalletGuid))
            {
                result.Add(transaction.ToDto());
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<TransactionResponse> GetAll()
    {
        try
        {
            List<TransactionResponse> result = new List<TransactionResponse>();
            foreach (Transaction transaction in _repo.GetAll())
            {
                result.Add(transaction.ToDto());
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    // I don't think Transaction needs update (or even delete),
    // might delete later (and change setters to private)
    public TransactionResponse Update(UpdateTransactionRequest request)
    {
        try
        {
            Transaction toUpdate = _repo.GetByGuid(request.Guid);
            toUpdate.Amount = request.Amount;
            toUpdate.Category = request.Category;
            toUpdate.Description =  request.Description;
            toUpdate.Date = request.Date;
            return _repo.Update(toUpdate).ToDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void Delete(DeleteTransactionRequest request)
    {
        try
        {
            _repo.Delete(request.Guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}