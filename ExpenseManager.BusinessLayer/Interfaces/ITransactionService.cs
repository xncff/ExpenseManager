using ExpenseManager.BusinessLayer.Dtos;

namespace ExpenseManager.BusinessLayer.Interfaces
{
    public interface ITransactionService
    {
        TransactionResponse Create(CreateTransactionRequest request);
        TransactionResponse GetByGuid(GetTransactionRequest request);
        IEnumerable<TransactionResponse> GetAllByWallet(GetTransactionByWalletRequest request);
        IEnumerable<TransactionResponse> GetAll();
        TransactionResponse Update(UpdateTransactionRequest request);
        void Delete(DeleteTransactionRequest request);
    }
}