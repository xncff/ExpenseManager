using ExpenseManager.BusinessLayer.Models;
using ExpenseManager.BusinessLayer.Interfaces;

namespace ExpenseManager.BusinessLayer.Services;

public class TransactionService
{
    private readonly ITransactionRepo _repo;

    public TransactionService(ITransactionRepo repo)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    public Transaction Create(Guid walletId, int amount, ExpenseType expenseType, string description)
    {
        try
        {
            Transaction toCreate = new Transaction(walletId, amount, expenseType, description);
            return _repo.Create(toCreate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Transaction GetByGuid(Guid guid)
    {
        try
        {
            return  _repo.GetByGuid(guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Transaction> GetAllByWalletGuid(Guid walletGuid)
    {
        try
        {
            return _repo.GetAllByWalletGuid(walletGuid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Transaction> GetAll()
    {
        try
        {
            return _repo.GetAll();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Transaction Update(Guid guid, int amount, ExpenseType expenseType, string description, DateTime date)
    {
        try
        {
            Transaction toUpdate = _repo.GetByGuid(guid);
            toUpdate.Amount = amount;
            toUpdate.ExpenseType = expenseType;
            toUpdate.Description = description;
            toUpdate.Date = date;
            return _repo.Update(toUpdate);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void Delete(Guid guid)
    {
        try
        {
            _repo.Delete(guid);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}