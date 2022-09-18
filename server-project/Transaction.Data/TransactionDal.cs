using CustomExceptions;
using Microsoft.EntityFrameworkCore;

using Transaction.Data.Entities;
using Transaction.Data.Interfaces;

namespace Transaction.Data;
public class TransactionDal : ITransactionDal
{
    private readonly IDbContextFactory<TransactionContext> _factory;

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
            throw new NotSavedException("didnt add transation");
        }
    }

    public async Task UpdateStatus(int transactionID, Status status, string? failureReason)
    {
        using var _context = _factory.CreateDbContext();
        try
        {
            var transaction = await _context.Transactions.FirstAsync(t => t.TransactionID == transactionID);
            transaction.Status = status;
            transaction.FailureReason = failureReason;
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new NoAccessException("fail to update status of transaction");
        }
    }
}
