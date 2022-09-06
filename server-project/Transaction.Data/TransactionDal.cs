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
    }
}
