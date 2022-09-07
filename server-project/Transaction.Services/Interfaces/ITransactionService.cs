using NServiceBus;
using Transaction.Services.Models;

namespace Transaction.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<bool> UpdateStatusTransaction(StausModel staus);
        Task AddTransaction(TransactionModel transactionModel, IMessageSession _session);
    }
}
