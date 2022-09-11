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

    public async Task<List<OperationsHistory>> GetOperations(int id,int page,int records)
    {
        try
        {
            using var _contect = _contextFactory.CreateDbContext();

            var position = (page - 1) * records;
            List <OperationsHistory > nextPage = _contect.OperationsHistorys
              .OrderBy(b => b.OperationTime)
              .Where(op => op.AccountId == id)
              .Skip(position)
              .Take(records)
              .ToList();



            return nextPage;
        }
        catch(Exception ex)
        {
            throw ex;
        }
        

    }


}

