using Transaction.Data.Entities;

namespace Transaction.Data.Interfaces;
public interface ITransactionDal
{
    Task<int> AddTransaction(Entities.Transaction transaction);
    Task UpdateStatus(int transactionID,Status status,string? failureReason);
}
