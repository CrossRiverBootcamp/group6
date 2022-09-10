using NServiceBus;
using Transaction.Services.Models;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> UpdateStatusTransaction(StausModel staus);
        Task<bool> AddTransaction(TransactionModel transactionModel, IMessageSession session);
        Task<int> AddTransactionToDB(TransactionModel transactionModel);
    }
}
