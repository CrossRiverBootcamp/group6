using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
using CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccount.Data;

public class OperationsHistoryDal : IOperationsHistoryDal
{
    private readonly IDbContextFactory<CustomerAccountContext> _contextFactory;

    public OperationsHistoryDal(IDbContextFactory<CustomerAccountContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task AddOperationsHistorys(OperationsHistory operationsHistoryFrom, OperationsHistory operationsHistoryTo)
    {
        using var _contect = _contextFactory.CreateDbContext();
        try
        {
            await _contect.OperationsHistorys.AddAsync(operationsHistoryFrom);
            await _contect.OperationsHistorys.AddAsync(operationsHistoryTo);
            await _contect.SaveChangesAsync();
        }
        catch
        {
            throw new NotSavedException("fail to add to operation history");
        }
    }

    public async Task<List<OperationsHistory>> GetOperations(int accountID, int page, int records)
    {

        using var _contect = _contextFactory.CreateDbContext();

        var position = page * records;

        var nextPage = from toOpt in _contect.OperationsHistorys
                       join fromOpt in _contect.OperationsHistorys
                       on toOpt.TransactionID equals fromOpt.TransactionID
                       where fromOpt.AccountId == accountID && toOpt.AccountId != fromOpt.AccountId
                       orderby fromOpt.OperationTime
                       select new OperationsHistory()
                       {
                           ID = fromOpt.ID,
                           AccountId = toOpt.AccountId,
                           TransactionID = fromOpt.TransactionID,
                           Credit = fromOpt.Credit,
                           TransactionAmount = fromOpt.TransactionAmount,
                           Balance = fromOpt.Balance,
                           OperationTime = fromOpt.OperationTime
                       };
        try
        {
            var operations = await nextPage.Skip(position)
                            .Take(records)
                            .ToListAsync();
            return operations;
        }
        catch
        {
            throw new NoAccessException("no access to get operations");
        }
    }
    public async Task<int> GetNumOfOperations(int accountID)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {
            return await _context.OperationsHistorys.Where(op => op.AccountId == accountID).CountAsync();
        }
        catch
        {
            throw new NoAccessException("no access to get num of operations");
        }
    }
}

