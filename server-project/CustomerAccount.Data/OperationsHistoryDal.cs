using CustomerAccount.Data.Entities;
using CustomerAccount.Data.Interfaces;
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
        await _contect.OperationsHistorys.AddAsync(operationsHistoryFrom);
        await _contect.OperationsHistorys.AddAsync(operationsHistoryTo);
        try
        {
            await _contect.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<List<OperationsHistory>> GetOperations(int id, int page, int records)
    {
        try
        {
            using var _contect = _contextFactory.CreateDbContext();

            var position = page * records;

            var nextPage = from toOpt in _contect.OperationsHistorys
                           join fromOpt in _contect.OperationsHistorys
                           on toOpt.TransactionID equals fromOpt.TransactionID
                           where fromOpt.AccountId == id && toOpt.AccountId != fromOpt.AccountId
                           orderby fromOpt.OperationTime
                           select new OperationsHistory()
                           {
                               ID = toOpt.ID,
                               AccountId = toOpt.AccountId,
                               TransactionID = toOpt.TransactionID,
                               Credit = toOpt.Credit,
                               TransactionAmount = toOpt.TransactionAmount,
                               Balance = toOpt.Balance,
                               OperationTime = toOpt.OperationTime
                           };
          var result = nextPage.Skip(position)
                     .Take(records)
                     .ToList();


            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public async Task<int> GetNumOfOperations(int id)
    {
        using var _context = _contextFactory.CreateDbContext();
        try
        {
            return await _context.OperationsHistorys.Where(op => op.AccountId == id).CountAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }


}

