using Microsoft.EntityFrameworkCore;

using Transaction.Data.Entities;
using Transaction.Data.Interfaces;

namespace Transaction.Data
{
    public class TransactionDal : ITransactionDal
    {
        private IDbContextFactory<TransactionContext> _factory;

        public TransactionDal(IDbContextFactory<TransactionContext> factory)
        {
            _factory = factory;
        }
        public async Task<int> AddTransaction(Entities.Transaction transaction)
        {
            using var _context = _factory.CreateDbContext();
            try
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
                return transaction.TransactionID;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> UpdateStatus(int transactionID, Status status, string? failureReason)
        {
            using var _context = _factory.CreateDbContext();
            try
            {
                var transaction = _context.Transactions.FirstAsync(t => t.TransactionID == transactionID).Result;
                transaction.Status = status;
                transaction.FailureReason = failureReason;
               await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
